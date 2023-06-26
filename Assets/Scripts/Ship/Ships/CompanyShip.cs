using OfFogAndDust.Ship.Base;
using OfFogAndDust.Ship.Data;
using UnityEngine;

namespace OfFogAndDust.Ship.Ships
{
    internal class CompanyShip : ShipBase
    {
        internal new void Start()
        {
            base.Start();
        }

        // TEST FUNCTION
        //private void Update()
        //{
        //    Debug.Log(IsTaskAssigned(ShipTask.ShipTaskName.Maneuver)
        //        + " " + IsTaskAssigned(ShipTask.ShipTaskName.Weapons)
        //        + " " + IsTaskAssigned(ShipTask.ShipTaskName.Repair));
        //}
    }
}
