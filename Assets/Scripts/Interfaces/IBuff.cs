using Builders;
namespace Interfaces
{
    public interface IBuff
    {
        public int AlterDamageReceived(int amount, Affinity attackAffinity);
    }
}