using System.Numerics;

namespace Clicker
{
    public interface IWallet
    {
        Currency Amount { get; }
        bool TryPurchase(Currency price);
    }
}