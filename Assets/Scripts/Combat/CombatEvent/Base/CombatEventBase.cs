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
        internal float timeIssued;
        internal float duration;

        public int CompareTo(object obj)
        {
            if (state == State.Paused)
            {
                return -1;
            } 
            else
            {
                return (timeIssued + duration).CompareTo(((CombatEventBase)obj).timeIssued + ((CombatEventBase)obj).duration);
            }
        }

        internal abstract void Trigger();

        public void Delay(float currentTime)
        {
            state = State.Paused;
            duration -= currentTime - timeIssued; // calculate the remaining duration
        }

        public void Resume(float currentTime)
        {
            state = State.Running;
            timeIssued = currentTime;
        }
    }
}
