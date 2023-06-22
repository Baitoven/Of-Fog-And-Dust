using OfFogAndDust.Combat.CombatEvent.Base;
using System.Collections.Generic;

namespace OfFogAndDust.Combat
{
    internal class CombatEventManager
    {
        private List<CombatEventBase> eventQueue = new List<CombatEventBase>();

        public void AddEvent(CombatEventBase newEvent) 
        {
            newEvent.timeIssued = CombatManager.Instance.clock.CurrentTime;
            eventQueue.Add(newEvent);
            eventQueue.Sort();
        }

        internal void TriggerEvent(float currentTime)
        {
            if (eventQueue.Count > 0 && eventQueue[0].timeIssued + eventQueue[0].duration < currentTime) // based on the fact that events are sorted
            {
                eventQueue[0].Trigger();
                eventQueue.RemoveAt(0);

                TriggerEvent(currentTime);
            }
        }
    }
}
