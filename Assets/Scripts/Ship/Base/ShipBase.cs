using Assets.Scripts.Ship.Data;
using OfFogAndDust.Ship.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Ship.Base
{
    internal class ShipBase : MonoBehaviour, IShip
    {
        internal Dictionary<ShipStatus.ShipStatusName, ShipStatus> shipStats;

        public void Start() { }

    }
}
