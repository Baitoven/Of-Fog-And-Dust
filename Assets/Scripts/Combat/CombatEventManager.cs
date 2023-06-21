using OfFogAndDust.Combat.CombatEvent.Base;
using System.Collections.Generic;

namespace OfFogAndDust.Combat
{
    internal class CombatEventManager
    {
        private List<CombatEventBase> eventQueue = new List<CombatEventBase>();

        public void AddEvent(CombatEventBase newEvent) 
        {
            eventQueue.Add(newEvent);
            eventQueue.Sort();
        }
    }
}
