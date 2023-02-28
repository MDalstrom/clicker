namespace Clicker
{
    public class Wallet : IWallet
    {
        public Currency Amount { get; }
        public bool TryPurchase(Currency price)
        {
            if (price.Value > Amount.Value)
                return false;
            
            Amount.Subtract(price);
            return true;
        }
    }
}