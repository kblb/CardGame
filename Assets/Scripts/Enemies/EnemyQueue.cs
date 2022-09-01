using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Enemies
{
    public class EnemyQueue : MonoBehaviour
    {
        private List<EnemyView> _enemies;
        private Transform _thisTransform;

        private void Awake()
        {
            _enemies = new List<EnemyView>();
            _thisTransform = transform;
        }

        public void AddEnemy(EnemyView enemyView)
        {
            _enemies.Add(enemyView);
            Redraw();
        }

        private void Redraw()
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                var enemyView = _enemies[i];
                enemyView.transform.position = _thisTransform.position + new Vector3(i * 10f, 0, -5f + 0.1f * i);
            }
        }

        public void Attack(List<Card> cards)
        {
            Debug.Log("Attacking");
            foreach (var card in cards)
            {
                var enemyView = _enemies[0];
                if (enemyView.Attack(card))
                {
                    Debug.Log("Died");
                    _enemies.RemoveAt(0);
                }
                Redraw();
            }
        }
    }
}