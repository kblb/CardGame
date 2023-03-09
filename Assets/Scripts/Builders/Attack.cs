// Copyright (c) CD PROJEKT S. A. All Rights Reserved.

namespace Builders
{
    public class Attack
    {
        public int damage;
        public Affinity affinity;

        public Attack()
        {
            damage = 0;
            affinity = Affinity.None;
        }
        
        public Attack(Attack copy)
        {
            damage = copy.damage;
            affinity = copy.affinity;
        }

        public override string ToString()
        {
            return $"Attack [{affinity};{damage}]";
        }
    }
}