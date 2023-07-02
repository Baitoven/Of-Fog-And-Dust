using OfFogAndDust.Combat.CombatEvent.Base;
using OfFogAndDust.Ship.Data;
using UnityEngine;

namespace OfFogAndDust.Combat.CombatEvent
{
    internal class ShipRepairEvent : CombatEventBase
    {
        internal override ShipTask.ShipTaskName EventToTaskName()
        {
            return ShipTask.ShipTaskName.Repair;
        }

        internal override void Trigger()
        {
            Debug.Log("Company Ship repaired");
            // do some stuff when event is triggered

            CombatManager.Instance.eventManager.AddOrResumeEvent(new ShipRepairEvent
            {
                duration = 4f,
                timeRemaining = 4f
            });
        }
    }
}
