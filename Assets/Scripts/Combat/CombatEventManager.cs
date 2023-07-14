using OfFogAndDust.Combat.CombatEvent.Base;
using OfFogAndDust.Ship.Displayers;
using System;
using System.Collections.Generic;
using static OfFogAndDust.Combat.CombatEvent.Base.CombatEventBase;

namespace OfFogAndDust.Combat
{
    internal class CombatEventManager
    {
        private List<CombatEventBase> eventQueue = new List<CombatEventBase>();

        internal void UpdateEventTick(float deltaTime, ShipDisplay display)
        {
            // Decrease time on each running event
            foreach (CombatEventBase eventBase in eventQueue)
            {
                if (eventBase.state == State.Running)
                {
                    eventBase.timeRemaining -= deltaTime;

                    display.UpdateTaskDisplay(eventBase.EventToTaskName(), 1 - (float)(eventBase.timeRemaining / eventBase.duration));
                }
            }

            eventQueue.Sort();
        }

        public void AddOrResumeEvent<T>(T newEvent) where T : CombatEventBase
        {
            CombatEventBase combatEvent = FindEvent(new FindEventSearch
            {
                isEnemy = newEvent.isEnemy,
                type = typeof(T),
            });
            if (combatEvent == null)
            {
                eventQueue.Add(newEvent);
            }
            else
            {
                combatEvent.Resume();
            }
            eventQueue.Sort();
        }

        // TO IMPLEMENT: Add check to see if task is paused
        internal void TriggerEvent(float currentTime)
        {
            if (eventQueue.Count > 0 && eventQueue[0].state == State.Running && eventQueue[0].timeRemaining < 0) // based on the fact that events are sorted
            {
                CombatEventBase triggeredEvent = eventQueue[0];
                eventQueue.RemoveAt(0);
                triggeredEvent.Trigger();

                // tries to trigger next events if simultaneous
                TriggerEvent(currentTime);
            }
        }

        internal class FindEventSearch
        {
            internal Type type;
            internal bool? isEnemy;
            internal State? state;
        }

        internal CombatEventBase FindEvent(FindEventSearch search) 
        {
            foreach (CombatEventBase combatEvent in eventQueue) 
            {
                if (combatEvent.GetType() == search.type 
                    && (search.state == null || combatEvent.state == search.state) 
                    && (search.isEnemy == null || combatEvent.isEnemy == search.isEnemy))
                {
                    return combatEvent;
                }
            }
            return null;
        }

        internal class DelayEventSearch
        {
            internal bool isEnemy = false;
            internal Type type;
        }

        internal void DelayEvent(FindEventSearch search)
        {
            CombatEventBase searchedEvent = FindEvent(search);
            searchedEvent?.Delay();
        }
    }
}
