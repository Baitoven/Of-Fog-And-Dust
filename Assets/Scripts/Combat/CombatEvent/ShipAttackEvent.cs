using OfFogAndDust.Combat.CombatEvent.Base;
using OfFogAndDust.Ship;
using OfFogAndDust.Ship.Data;
using UnityEngine;

namespace OfFogAndDust.Combat.CombatEvent
{
    internal class ShipAttackEvent : CombatEventBase
    {
        internal override ShipTask.ShipTaskName EventToTaskName()
        {
            return ShipTask.ShipTaskName.Weapons;
        }

        internal override void Trigger()
        {
            // TODO: Ajust Weapon Damage
            ShipManager.Instance.TriggerEffect(ShipTask.ShipTaskName.Weapons, isEnemy, 1);

            CombatManager.Instance.eventManager.AddOrResumeEvent(new ShipAttackEvent
            {
                duration = 8f,
                timeRemaining = 8f,
                isEnemy = isEnemy
            });
        }
    }
}
