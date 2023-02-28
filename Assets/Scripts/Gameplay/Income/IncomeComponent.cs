using Clicker.Gameplay.Upgrading;

namespace Clicker.Gameplay.Income
{
    public struct IncomeComponent
    {
        public int Level { get; set; }
        
        public Currency BaseIncome { get; set; }
        public float Multiplier { get; set; }
        public Currency Income => new(Level * BaseIncome.Value * (decimal)(1 + Multiplier));
        
        public Currency BasePrice { get; set; }
        public Currency Price => new((Level + 1) * BasePrice.Value);

    }
}