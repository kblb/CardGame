// Copyright (c) CD PROJEKT S. A. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;
namespace Builders
{
    public class AttackCollection
    {
        public readonly Dictionary<int, Attack> attacks;

        public AttackCollection(Dictionary<int, Attack> attacks) {
            this.attacks = attacks;
        }

        public void Execute(BattleInstance battleInstance)
        {
            foreach (var (index, attack) in attacks)
            {
                if (index == AttackBuilder.PLAYER_TARGET_INDEX)
                {
                    Debug.Log($"Targeting player with {attack}");
                    battleInstance.Player.ReceiveDamage(attack);
                } 
                else if (battleInstance.allEnemies.Count > index)
                {
                    Debug.Log($"Targeting enemy {index} with {attack}");
                    battleInstance.allEnemies[index].ReceiveDamage(attack);
                }
                else
                {
                    Debug.Log($"AttackCollection: No enemy at position {index}");
                }
            }
        }
    }
}