// Copyright (c) CD PROJEKT S. A. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;
namespace Builders
{
    public class AttackCollection
    {
        public readonly Dictionary<int, Attack> attacks;

        public AttackCollection(Dictionary<int, Attack> attacks)
        {
            this.attacks = attacks;
        }

        public void Execute(BattleInstance battleInstance)
        {
            var deaths = new List<int>();
            foreach (var (index, attack) in attacks)
            {
                var isDead = false;
                if (index == AttackBuilder.PLAYER_TARGET_INDEX)
                {
                    Debug.Log($"Targeting player with {attack}");
                    isDead = battleInstance.Player.ReceiveDamage(attack);
                }
                else if (battleInstance.allEnemies.Count > index)
                {
                    Debug.Log($"Targeting enemy {index} with {attack}");
                    isDead = battleInstance.allEnemies[index].ReceiveDamage(attack);
                }
                else
                {
                    Debug.Log($"AttackCollection: No enemy at position {index}");
                }

                if (isDead)
                    deaths.Add(index);
            }

            foreach (var death in deaths)
            {
                if (death == AttackBuilder.PLAYER_TARGET_INDEX)
                {
                    battleInstance.Player.Die();
                }
                else if (battleInstance.allEnemies.Count > death)
                {
                    battleInstance.allEnemies[death].Die();
                }
            }
        }
    }
}