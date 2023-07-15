using OfFogAndDust.Combat.CombatEvent.Base;
using OfFogAndDust.Ship;
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
            ShipManager.Instance.TriggerEffect(ShipTask.ShipTaskName.Repair, isEnemy);

            CombatManager.Instance.eventManager.AddOrResumeEvent(new ShipRepairEvent
            {
                duration = 4f,
                timeRemaining = 4f,
                isEnemy = isEnemy
            });
        }
    }
}
