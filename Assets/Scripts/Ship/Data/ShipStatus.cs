namespace Assets.Scripts.Ship.Data
{
    internal class ShipStatus
    {
        public ShipStatusName Name { get; set; }
        public double maxValue;
        public double minValue;
        public double currentValue;

        internal enum ShipStatusName
        {
            Health,
            Armor,
            Fuel
        }
    }
}
