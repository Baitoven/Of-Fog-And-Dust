using OfFogAndDust.Ship.Data;
using System;

namespace OfFogAndDust.Combat.CombatEvent.Base
{
    internal abstract class CombatEventBase : IComparable
    {
        internal enum State
        {
            Running,
            Paused,
        }

        internal State state = State.Running;
        internal bool isEnemy = false;
        internal float timeRemaining;
        internal float duration;

        internal abstract void Trigger();
        internal abstract ShipTask.ShipTaskName EventToTaskName();

        public int CompareTo(object obj)
        {
            return timeRemaining.CompareTo(((CombatEventBase)obj).timeRemaining);
        }

        public void Delay()
        {
            state = State.Paused;
        }

        public void Resume()
        {
            state = State.Running;
        }
    }
}
