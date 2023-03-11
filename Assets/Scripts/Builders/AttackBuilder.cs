using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
namespace Builders
{

    public class InitialTargetSelectionPolicy
    {
        public static readonly InitialTargetSelectionPolicy Default = new InitialTargetSelectionPolicy
        {
            isPlayerTargeted = false,
            isEnemyTargeted = new List<bool>() { true, false, false, false, false }
        };
        
        public bool isPlayerTargeted;
        public List<bool> isEnemyTargeted;
    }

    public class AttackBuilder
    {
        public const int PLAYER_TARGET_INDEX = -1;

        private readonly Attack baseAttack;

        private readonly List<bool> isEnemyTargeted;
        private bool isPlayerTargeted;
        
        private readonly List<Affinity> affinities;
        private readonly List<int> additiveDamageModifiers;
        private readonly List<float> multiplicativeDamageModifiers;
        private int? damageOverride;

        public AttackBuilder(BattleInstance battleInstance, Attack baseAttack, InitialTargetSelectionPolicy policy)
        {
            isEnemyTargeted = new List<bool>(battleInstance.allEnemies.Count);
            for (int i = 0; i < battleInstance.allEnemies.Count; i++)
            {
                isEnemyTargeted.Add(policy.isEnemyTargeted[i]);
            }
            isPlayerTargeted = policy.isPlayerTargeted;
            this.baseAttack = baseAttack;
            affinities = new List<Affinity>();
            additiveDamageModifiers = new List<int>();
            multiplicativeDamageModifiers = new List<float>();
            damageOverride = null;
        }
        
        public void AddDamage(int amount)
        {
            additiveDamageModifiers.Add(amount);
        }
        
        public void HealDamage(int amount)
        {
            additiveDamageModifiers.Add(-amount);
        }
        
        public void SetDamage(int amount)
        {
            damageOverride = amount;
        }

        public void MultiplyDamage(float amount)
        {
            multiplicativeDamageModifiers.Add(amount);
        }
        
        public void DivideDamage(float amount)
        {
            multiplicativeDamageModifiers.Add(1 / amount);
        }

        public void AddTarget(int target)
        {
            if (target == PLAYER_TARGET_INDEX)
            {
                isPlayerTargeted = true;
                return;
            }

            if (target >= isEnemyTargeted.Count)
            {
                Debug.LogError($"Target {target} is out of range");
                return;
            }
            
            isEnemyTargeted[target] = true;
        }
        
        public void AddTargetRelative()
        {
            int idx = isEnemyTargeted.LastIndexOf(true) + 1;
            if (idx >= isEnemyTargeted.Count)
            {
                Debug.LogError($"Target {idx} is out of range");
                return;
            }
            
            isEnemyTargeted[idx] = true;
        }

        public void AddTargets(int[] targets)
        {
            foreach (var target in targets)
            {
                AddTarget(target);
            }
        }
        
        public void RemoveTarget(int target)
        {
            if (target == PLAYER_TARGET_INDEX)
            {
                isPlayerTargeted = false;
                return;
            }
            
            if (target >= isEnemyTargeted.Count)
            {
                Debug.LogError($"Target {target} is out of range");
                return;
            }
            
            isEnemyTargeted[target] = false;
        }
        
        public void RemovePlayerTarget()
        {
            isPlayerTargeted = false;
        }
        
        public void RemoveEnemiesTarget()
        {
            for (int i = 0; i < isEnemyTargeted.Count; i++)
            {
                isEnemyTargeted[i] = false;
            }
        }

        public void AppendAffinity(Affinity affinity)
        {
            affinities.Add(affinity);
        }

        public AttackCollection BuildAttack()
        {
            var collection = new Dictionary<int, Attack>();
            var builtAttack = new Attack(baseAttack);
            
            Debug.Log($"Building attack from base {baseAttack}");
            if (damageOverride.HasValue)
            {
                Debug.Log("Applying damage override");
                builtAttack.damage = damageOverride.Value;
            }
            else
            {
                Debug.Log("Applying damage modifiers");
                builtAttack.damage = baseAttack.damage;
                foreach (var modifier in additiveDamageModifiers)
                {
                    builtAttack.damage += modifier;
                }

                foreach (var modifier in multiplicativeDamageModifiers)
                {
                    builtAttack.damage = Mathf.RoundToInt(builtAttack.damage * modifier);
                }
            }
            
            Debug.Log($"New attack damage {builtAttack.damage}; Applying {affinities.Count} affinities");
            
            foreach (var affinity in affinities)
            {
                Debug.Log($"Applying affinity {affinity} to {builtAttack.affinity}");
                builtAttack.affinity = builtAttack.affinity.CombineWith(affinity);
                Debug.Log($"New affinity: {builtAttack.affinity}");
            }

            Debug.Log($"Built attack {builtAttack}");

            if (isPlayerTargeted)
            {
                collection.Add(PLAYER_TARGET_INDEX, builtAttack);
            }
            
            for (int i = 0; i < isEnemyTargeted.Count; i++)
            {
                if (isEnemyTargeted[i])
                {
                    collection.Add(i, builtAttack);
                }
            }
            
            Debug.Log($"Built attack collection with {collection.Count} attacks");

            return new AttackCollection(collection);
        }

        public override string ToString()
        {
            string result = $"{baseAttack.affinity};{baseAttack.damage} => ";
            if (isPlayerTargeted)
            {
                result += "[x] ... ";
            }
            else
            {
                result += "[ ] ... ";
            }
            for (int i = 0; i < isEnemyTargeted.Count; i++)
            {
                if (isEnemyTargeted[i])
                {
                    result += "[x] ";
                }
                else
                {
                    result += "[ ] ";
                }
            }
            return result;
        }
    }
}