using OfFogAndDust.Combat;
using OfFogAndDust.Combat.CombatEvent;
using OfFogAndDust.Combat.CombatEvent.Base;
using System;
using UnityEngine;

namespace OfFogAndDust.Ship.Data
{
    [RequireComponent(typeof(Collider2D))]
    internal class ShipTask : MonoBehaviour
    {
        internal enum ShipTaskName
        {
            Maneuver,
            Repair,
            Weapons
        }

        public ShipTaskName taskName;
        public bool isEnemy;
        internal Character assigned;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Character collidedCharacter) && assigned == null)
            {
                assigned = collidedCharacter;

                CombatManager.Instance.AddOrResumeEvent(TaskToEvent(taskName));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Character _) && assigned != null)
            {
                assigned = null;

                CombatManager.Instance.DelayEvent(TaskToEventType(taskName));
            }
        }

        internal CombatEventBase TaskToEvent(ShipTaskName shipTask) {
            return shipTask switch
            {
                ShipTaskName.Maneuver => throw new NotImplementedException(),
                ShipTaskName.Repair => new ShipRepairEvent { duration = 4f, timeRemaining = 4f, isEnemy = isEnemy },
                ShipTaskName.Weapons => new ShipAttackEvent { duration = 8f, timeRemaining = 8f, isEnemy = isEnemy },
                _ => throw new ArgumentException(),
            };
        }

        internal Type TaskToEventType(ShipTaskName shipTask)
        {
            return shipTask switch
            {
                ShipTaskName.Maneuver => throw new NotImplementedException(),
                ShipTaskName.Repair => typeof(ShipRepairEvent),
                ShipTaskName.Weapons => typeof(ShipAttackEvent),
                _ => throw new ArgumentException(),
            };
        }
    }
}
