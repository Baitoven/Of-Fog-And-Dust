using OfFogAndDust.Combat;
using UnityEngine;
using static OfFogAndDust.Ship.Ship;

namespace OfFogAndDust.Ship
{
    internal class ShipTask : MonoBehaviour
    {
        public ShipTaskName taskName;
        public Collider2D collider;
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
