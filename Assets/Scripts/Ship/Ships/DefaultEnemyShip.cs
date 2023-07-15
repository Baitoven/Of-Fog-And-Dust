using Assets.Scripts.Ship.Data;
using OfFogAndDust.Ship.Base;
using OfFogAndDust.Ship.Displayers;
using OfFogAndDust.Ship.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Ship.Data.ShipStatus;

namespace Assets.Scripts.Ship.Ships
{
    internal class DefaultEnemyShip : ShipBase, IShip
    {
        private readonly Dictionary<ShipStatusName, ShipStatus> defaultStats = new Dictionary<ShipStatusName, ShipStatus>()
        {
            { ShipStatusName.Health, new ShipStatus
                { Name = ShipStatusName.Health, maxValue = 10, currentValue = 10 } },
            { ShipStatusName.Armor, new ShipStatus
                { Name = ShipStatusName.Armor, maxValue = 10, currentValue = 0, upValue = 2 } },
            { ShipStatusName.Fuel, new ShipStatus
                { Name = ShipStatusName.Fuel, maxValue = 10, currentValue = 5 } },
        };

        internal new void Start()
        {
            base.Start();
            shipStats = defaultStats;
            RefreshStatDisplay();
        }
    }
}
