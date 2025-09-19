using UnityEngine;

namespace OfFogAndDust.Shipment
{
    public class ShipmentManager : MonoBehaviour
    {
        public static ShipmentManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
