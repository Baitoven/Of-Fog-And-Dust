using OfFogAndDust.Combat.CombatEvent.Base;
using UnityEngine;

namespace OfFogAndDust.Combat.CombatEvent
{
    internal class ShipAttackEvent : CombatEventBase
    {
        internal override void Trigger()
        {
            Debug.Log("Company Ship fired");
            // do some stuff when event is triggered

            CombatManager.Instance.eventManager.AddEvent(new ShipAttackEvent
            {
                duration = 8f
            });
        }
    }
}
