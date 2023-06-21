using System;

namespace OfFogAndDust.Combat.CombatEvent.Base
{
    internal class CombatEventBase : IComparable
    {
        internal enum Type
        {
            ShipAttack,
        }

        internal Type type;
        internal float timeIssued;
        internal float timeTarget;

        public int CompareTo(object obj)
        {
            return timeTarget.CompareTo(((CombatEventBase)obj).timeTarget);
        }
    }
}
