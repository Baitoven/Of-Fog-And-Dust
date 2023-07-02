using OfFogAndDust.Ship.Base;
using OfFogAndDust.Ship.Data;
using OfFogAndDust.Ship.Displayers;
using UnityEngine;

namespace OfFogAndDust.Ship.Ships
{
    internal class CompanyShip : ShipBase
    {
        [SerializeField] private ShipDisplay display;

        internal new void Start()
        {
            base.Start();
        }

        // TEST FUNCTION
        private void Update()
        {
            if (display != null)
            {

            }
        }
    }
}
