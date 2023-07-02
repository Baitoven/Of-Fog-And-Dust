﻿using OfFogAndDust.Combat.CombatEvent.Base;
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
            Debug.Log("Company Ship fired");
            // do some stuff when event is triggered

            CombatManager.Instance.eventManager.AddOrResumeEvent(new ShipAttackEvent
            {
                duration = 8f,
                timeRemaining = 8f
            });
        }
    }
}
