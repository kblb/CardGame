using System.Collections.Generic;
using UnityEngine;
namespace Builders
{
    public enum Affinity
    {
        None,
        Fire,
        Water,
        Earth,
        Air,
        Light,
        Dark
    }
    
    public class Attack
    {
        public int damage;
        public Affinity affinity;
    }

    public class AttackBuilder
    {
        public const int PLAYER_TARGET_INDEX = -1;

        /// <summary>
        /// Attack targeted to enemies
        /// </summary>
        private List<Attack> attacksOnEnemies;
        /// <summary>
        ///  Attack targeted to player
        /// </summary>
        private Attack attackOnPlayer; 

        public AttackBuilder(BattleInstance battleInstance)
        {
            attacksOnEnemies = new List<Attack>(battleInstance.allEnemies.Count);
            for (int i = 0; i < battleInstance.allEnemies.Count; i++)
            {
                attacksOnEnemies.Add(new Attack());
            }
            attackOnPlayer = new Attack();
        }
        
        public void AddDamage(int amount, int target)
        {
            if (target == PLAYER_TARGET_INDEX)
            {
                attackOnPlayer.damage += amount;
            }
            else
            {
                Attack enemy = attacksOnEnemies[target];
                enemy.damage += amount;
            }
        }
        
        public void SetDamage(int amount, int target)
        {
            if (target == PLAYER_TARGET_INDEX)
            {
                attackOnPlayer.damage = amount;
            }
            else
            {
                Attack enemy = attacksOnEnemies[target];
                enemy.damage = amount;
            }
        }
        
        public void HealDamage(int amount, int target)
        {
            if (target == PLAYER_TARGET_INDEX)
            {
                attackOnPlayer.damage -= amount;
            }
            else
            {
                Attack enemy = attacksOnEnemies[target];
                enemy.damage -= amount;
            }
        }

        public void SetAffinity(Affinity affinity)
        {
            attackOnPlayer.affinity = affinity;
            attacksOnEnemies.ForEach(attack => attack.affinity = affinity);
        }

        public void Execute(BattleInstance battleInstance)
        {
            for (int i = 0; i < Mathf.Min(attacksOnEnemies.Count, battleInstance.allEnemies.Count); i++)
            {
                Attack attack = attacksOnEnemies[i];
                battleInstance.allEnemies[i].ReceiveDamage(attack.damage, attack.affinity);
            }
            
            battleInstance.Player.ReceiveDamage(attackOnPlayer.damage, attackOnPlayer.affinity);
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < attacksOnEnemies.Count; i++)
            {
                Attack attack = attacksOnEnemies[i];
                result += $"Enemy {i}: {attack.damage} {attack.affinity} damage; ";
            }
            return result;
        }
    }
}