using System;
using System.Linq;
using DG.Tweening;
using Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies
{
    // Class wrapping Enemy model and it's view
    public class EnemyController : MonoBehaviour
    {

        [SerializeField] private EnemyStatsView statsView;
        [SerializeField] [SceneObjectsOnly] private GameObject modelPrefab;
        private float originalScale;
        private const float AttackScaleFactor = 1.2f;
        
        public EnemyModel RawEnemy { get; private set; }
        public EnemyModelInstance EnemyModelInstance { get; private set; }

        public Attack SelectedAttack => EnemyModelInstance.SelectedAttack;

        public void Init(EnemyModel enemy, EnemyModelInstance enemyModelInstance)
        {
            RawEnemy = enemy;
            EnemyModelInstance = enemyModelInstance;
            modelPrefab = Instantiate(enemy.GetModel, this.transform);
            originalScale = modelPrefab.transform.localScale.x;

            statsView.SetModel(EnemyModelInstance);
        }

        public void SelectNextAttack(PlayerModel playerModel, EnemyModel[] allEnemies, int myEnemyIndex)
        {
            EnemyModelInstance.SelectAttack(RawEnemy.GetNextAttack(playerModel, allEnemies, myEnemyIndex));
        }

        public void ApplyPassiveToQueue(PlayerModel playerModel, EnemyController[] queue)
        {
            if (RawEnemy.passive != null)
            {
                var applied = RawEnemy.passive.Passive(playerModel, queue.Select(e => e.RawEnemy).ToArray(), Array.IndexOf(queue, this));
                foreach (var (i, buff) in applied)
                {
                    queue[i].EnemyModelInstance.AddBuff(buff);
                }
            }
        }
        
        public bool CanApplyPassive()
        {
            return RawEnemy.passive != null;
        }

        public void Shield(float amount)
        {
            EnemyModelInstance.Shields += amount;
        }

        public void Heal(float flatHealAmount)
        {
            EnemyModelInstance.CurrentHealth = Mathf.Min(EnemyModelInstance.CurrentHealth + flatHealAmount,
                EnemyModelInstance.MaxHealth);
        }

        public void ShowAttackAnimation()
        {
            modelPrefab.transform.DOScale(Vector3.one * originalScale * AttackScaleFactor, 0.5f);
        }

        public void HideAttackAnimation()
        {
            modelPrefab.transform.DOScale(Vector3.one, 0.2f);
        }

        public void ShowBuffApplyAnimation()
        {
            statsView.buffBar.DOScale(Vector3.one * 1.2f, 0.5f);
        }

        public void HideBuffApplyAnimation()
        {
            statsView.buffBar.transform.DOScale(Vector3.one, 0.2f);
        }
    }
}