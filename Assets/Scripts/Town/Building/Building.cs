namespace OfFogAndDust.Town.Building
{
    internal class Building
    {
        internal enum BuildingName
        {
            Pub,
            Shipyard,
            Lighthouse,
            Dock,
            Harbor,
            Apoticary,
            Workshop,
            Church
        }

        internal enum BuildingState
        {
            None, // meaning building is not built yet
            Built // for building built
        }
    }
}
