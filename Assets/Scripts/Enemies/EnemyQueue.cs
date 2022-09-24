using System.Collections.Generic;
using System.Linq;
using Cards;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    public class EnemyQueue : MonoBehaviour
    {
        [SerializeField] [SceneObjectsOnly] private List<Transform> slots;
        [SerializeField] [AssetsOnly] private EnemyController enemyControllerPrefab;
        private List<EnemyController> _enemies;
        public IEnumerable<EnemyController> Enemies => _enemies;

        private void Awake()
        {
            _enemies = new List<EnemyController>();
        }

        public EnemyController AddEnemy(EnemyModel enemy, EnemyModelInstance enemyModelInstance, PlayerModel playerModel)
        {
            var enemyController = AddToNextFreeSlot(enemy, enemyModelInstance);
            if (enemyController != null) enemyController.SelectNextAttack(playerModel, _enemies.Select(x => x.RawEnemy).ToArray(), _enemies.Count - 1);
            return enemyController;

        }

        private EnemyController AddToNextFreeSlot(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            if (_enemies.Count >= slots.Count) return null;

            var instance = Instantiate(enemyControllerPrefab, slots[_enemies.Count]);
            instance.Init(enemy, enemyModelInstance);
            _enemies.Add(instance);

            return instance;
        }

        public void AttackEnemies(List<Card> cards)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var enemy = _enemies.First();
                if (!enemy.AttackEnemy(card)) continue;

                _enemies.RemoveAt(0);
                Destroy(enemy.gameObject);
            }
            Redraw();
        }

        private void Redraw()
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].transform.SetParent(slots[i]);
                _enemies[i].transform.localPosition = Vector3.zero;
            }
        }

        public Attack AttackPlayer(PlayerModel playerModel)
        {
            var enemy = _enemies.First();
            var attack = enemy.SelectedAttack;
            enemy.SelectNextAttack(playerModel, _enemies.Select(e => e.RawEnemy).ToArray(), 0);
            return attack;
        }

        public void PrepareNextRound(PlayerModel playerModel)
        {
            _enemies.First().SelectNextAttack(playerModel, _enemies.Select(e => e.RawEnemy).ToArray(), 0);

            for (var i = 0; i < _enemies.Count; i++)
            {
                var e = _enemies[i];
                foreach (var buff in e.EnemyModelInstance.Buffs) buff.ApplyEffect(e);
                e.EnemyModelInstance.ClearBuffs();
            }

            for (var i = 0; i < _enemies.Count; i++) _enemies[i].ApplyPassiveToQueue(playerModel, _enemies.ToArray(), i);
        }
    }
}