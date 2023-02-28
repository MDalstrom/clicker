namespace Clicker.Gameplay.Upgrading
{
    public struct PowerUpComponent
    {
        public int BoundBusiness { get; set; }
        public Currency Price { get; set; }
        public float Multiplier { get; set; }
        public bool IsPurchased { get; set; }
    }
}