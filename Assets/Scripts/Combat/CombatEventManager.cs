using OfFogAndDust.Combat.CombatEvent.Base;
using System;
using System.Collections.Generic;
using static OfFogAndDust.Combat.CombatEvent.Base.CombatEventBase;

namespace OfFogAndDust.Combat
{
    internal class CombatEventManager
    {
        private List<CombatEventBase> eventQueue = new List<CombatEventBase>();

        public void AddEvent<T>(T newEvent) where T : CombatEventBase
        {
            if (FindEvent(new FindEventSearch
            {
                isEnemy = newEvent.isEnemy,
                state = newEvent.state,
                type = typeof(T),
            }) == null)
            {
                newEvent.timeIssued = CombatManager.Instance.clock.CurrentTime;
                eventQueue.Add(newEvent);
            }
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

        internal class FindEventSearch
        {
            internal bool isEnemy = false;
            internal Type type;
            internal State state;
        }

#nullable enable
        internal CombatEventBase? FindEvent(FindEventSearch search) 
        {
            foreach (CombatEventBase combatEvent in eventQueue) 
            {
                if (combatEvent.GetType() == search.type && combatEvent.state == search.state && combatEvent.isEnemy == search.isEnemy)
                {
                    return combatEvent;
                }
            }
            return null;
        }
#nullable disable
    }
}
