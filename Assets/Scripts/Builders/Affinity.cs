using System;
namespace Builders
{
    public enum Affinity
    {
        None,
        CancelOut,
        Fire,
        Water,
        Earth,
        Air,
        Magma, // Fire + Earth
        Thunder, // Fire + Air
        Mud, // Water + Earth
        Ice, // Water + Air
        Transience, // Earth + Air
    }

    public static class AffinityExtensions
    {
        public static Affinity CombineWith(this Affinity affinity, Affinity other)
        {
            if (affinity == Affinity.None)
            {
                return other;
            }
            if (other == Affinity.None)
            {
                return affinity;
            }
            if (affinity == Affinity.CancelOut || other == Affinity.CancelOut)
            {
                return Affinity.CancelOut;
            }
            switch (affinity)
            {
                case Affinity.Fire when other == Affinity.Water:
                case Affinity.Water when other == Affinity.Fire:
                    return Affinity.CancelOut;
                case Affinity.Fire when other == Affinity.Earth:
                case Affinity.Earth when other == Affinity.Fire:
                    return Affinity.Magma;
                case Affinity.Fire when other == Affinity.Air:
                case Affinity.Air when other == Affinity.Fire:
                    return Affinity.Thunder;
                case Affinity.Water when other == Affinity.Earth:
                case Affinity.Earth when other == Affinity.Water:
                    return Affinity.Mud;
                case Affinity.Water when other == Affinity.Air:
                case Affinity.Air when other == Affinity.Water:
                    return Affinity.Ice;
                case Affinity.Earth when other == Affinity.Air:
                case Affinity.Air when other == Affinity.Earth:
                    return Affinity.Transience;
                case Affinity.Magma:
                case Affinity.Thunder:
                case Affinity.Mud:
                case Affinity.Ice:
                case Affinity.Transience:
                default:
                    return Affinity.None;
            }
        }

        public static float BaseMultiplier(this Affinity affinity) => affinity switch {
            Affinity.None => 1,
            Affinity.CancelOut => 0,
            Affinity.Fire => 1.2f,
            Affinity.Water => 1.1f,
            Affinity.Earth => 1.2f,
            Affinity.Air => 1.1f,
            Affinity.Magma => 1.4f,
            Affinity.Thunder => 1.5f,
            Affinity.Mud => 1.3f,
            Affinity.Ice => 1.2f,
            Affinity.Transience => 1.8f,
            _ => throw new ArgumentOutOfRangeException(nameof(affinity), affinity, null)
        };
        
        
        /// <summary>
        /// TODO: Convert this to a 2d matrix
        /// </summary>
        public static float MultiplierOnAttacking(this Affinity attackAffinity, Affinity actorAffinity)
        {
            if (attackAffinity == actorAffinity && attackAffinity != Affinity.None && attackAffinity != Affinity.CancelOut)
            {
                return 0.25f;
            }
            switch (attackAffinity)
            {
                case Affinity.Fire when actorAffinity == Affinity.Water:
                case Affinity.Fire when actorAffinity == Affinity.Earth:
                    return 0.1f;
                case Affinity.Water when actorAffinity == Affinity.Fire:
                    return 2.0f;
                case Affinity.Earth when actorAffinity == Affinity.Fire:
                    return 1.5f;
                default:
                    return attackAffinity.BaseMultiplier();
            }

        }
    }
}