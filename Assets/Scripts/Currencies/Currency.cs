namespace Clicker
{
    public struct Currency
    {
        public Currency(int value)
        {
            Value = value;
        }

        public Currency(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }

        public void Add(Currency other)
        {
            Value += other.Value;
        }

        public void Subtract(Currency other)
        {
            Value -= other.Value;
        }
    }
}