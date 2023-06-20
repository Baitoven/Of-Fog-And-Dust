using OfFogAndDust.Combat;
using UnityEngine;

namespace OfFogAndDust.Ship.Data
{
    internal class ShipTask : MonoBehaviour
    {
        internal enum ShipTaskName
        {
            Maneuver,
            Repair,
            Weapons
        }

        public ShipTaskName taskName;
        public new Collider2D collider;
        internal Character assigned;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Character collidedCharacter) && assigned == null)
            {
                assigned = collidedCharacter;
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
