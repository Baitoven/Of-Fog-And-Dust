using OfFogAndDust.Combat;
using OfFogAndDust.Combat.CombatEvent;
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
        internal Character assigned;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Character collidedCharacter) && assigned == null)
            {
                assigned = collidedCharacter;

                // TO IMPROVE
                if (taskName == ShipTaskName.Weapons)
                {
                    CombatManager.Instance.AddEvent<ShipAttackEvent>(new ShipAttackEvent { duration = 8f });
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Character collidedCharacter) && assigned != null)
            {
                assigned = null;
            }
        }
    }
}
