using Assets.Scripts.Ship.Data;
using OfFogAndDust.Ship.Base;
using OfFogAndDust.Ship.Displayers;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Ship.Ships
{
    internal class CompanyShip : ShipBase
    {
        [SerializeField] private ShipDisplay display;

        private Dictionary<ShipStatus.ShipStatusName, ShipStatus> defaultStats = new Dictionary<ShipStatus.ShipStatusName, ShipStatus>()
        {
            { ShipStatus.ShipStatusName.Health, new ShipStatus
                { Name = ShipStatus.ShipStatusName.Health, maxValue = 10, minValue = 0, currentValue = 10 } },
            { ShipStatus.ShipStatusName.Armor, new ShipStatus
                { Name = ShipStatus.ShipStatusName.Armor, maxValue = 10, minValue = 0, currentValue = 10 } },
            { ShipStatus.ShipStatusName.Fuel, new ShipStatus
                { Name = ShipStatus.ShipStatusName.Fuel, maxValue = 10, minValue = 0, currentValue = 10 } },
        };

        internal new void Start()
        {
            base.Start();
            display.NStart();
            shipStats = defaultStats;
            RefreshStatDisplay();
        }

        internal void UpdateStat(ShipStatus.ShipStatusName statName, double value)
        {
            if (shipStats.ContainsKey(statName))
            {
                shipStats[statName].currentValue = value;
                display.UpdateStatusDisplay(
                    statName,
                    StatValueToPercentile(shipStats[statName].maxValue, shipStats[statName].minValue, value)
                    );
            } 
            else
            {
                throw new KeyNotFoundException();
            }
        }

        internal void RefreshStatDisplay()
        {
            foreach (KeyValuePair<ShipStatus.ShipStatusName, ShipStatus> status in shipStats)
            {
                display.UpdateStatusDisplay(
                    status.Key, 
                    StatValueToPercentile(status.Value.maxValue, status.Value.minValue, status.Value.currentValue)
                    );
            }
        }

        internal float StatValueToPercentile(double maxValue, double minValue, double currentValue)
        {
            return (float)((currentValue - minValue) / (maxValue - minValue));
        }
    }
}
