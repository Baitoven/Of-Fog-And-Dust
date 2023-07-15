namespace Assets.Scripts.Ship.Data
{
    internal class ShipStatus
    {
        public ShipStatusName Name { get; set; }
        public double maxValue;
        public double upValue = 1d; // corresponds to the default value when increasing currentValue
        public double currentValue;

        internal enum ShipStatusName
        {
            Health,
            Armor,
            Fuel
        }
    }
}
