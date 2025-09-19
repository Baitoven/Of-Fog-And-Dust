namespace OfFogAndDust.Map.Events
{
    public class MapEvent
    {
        public enum EventTypeEnum
        {
            ENTRANCE,
            EXIT,
            DIALOG
        }
        public EventTypeEnum type;
        public int id = 0; // TODO
    }
}
