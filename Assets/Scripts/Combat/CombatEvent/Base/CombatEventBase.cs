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
        internal float timeIssued;
        internal float duration;

        public int CompareTo(object obj)
        {
            return (timeIssued + duration).CompareTo(((CombatEventBase)obj).timeIssued + ((CombatEventBase)obj).duration);
        }

        internal abstract void Trigger();

        public void Delay(float currentTime)
        {
            state = State.Paused;
            duration -= timeIssued - currentTime; // calculate the remaining duration
        }

        public void Resume(float currentTime)
        {
            state = State.Running;
            timeIssued = currentTime;
        }
    }
}
