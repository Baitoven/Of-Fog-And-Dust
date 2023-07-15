namespace OfFogAndDust.Ship.Interface
{
    internal interface IShip
    {
        public void Damage(int amount);
        public void Repair(int amount);
        public void ArmorUp();
    }
}
