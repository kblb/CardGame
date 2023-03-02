using Builders;
using Interfaces;
namespace InterfaceImplementations.Buff
{
    public class ElementAffinity : IBuff
    {
        public Affinity affinity;
        
        public int AlterDamageReceived(int amount, Affinity attackAffinity)
        {
            return attackAffinity == affinity ? 0 : amount;
        }
    }
}