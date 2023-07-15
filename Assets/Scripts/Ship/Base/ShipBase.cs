using Assets.Scripts.Ship.Data;
using OfFogAndDust.Ship.Displayers;
using OfFogAndDust.Ship.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Ship.Data.ShipStatus;

namespace OfFogAndDust.Ship.Base
{
    internal abstract class ShipBase : MonoBehaviour, IShip
    {
        [SerializeField] private ShipDisplay display;

        internal Dictionary<ShipStatus.ShipStatusName, ShipStatus> shipStats;

        internal void Start() 
        {
            display.NStart();
        }




        internal void UpdateStat(ShipStatusName statName, double value)
        {
            if (shipStats.ContainsKey(statName))
            {
                shipStats[statName].currentValue = value;
                display.UpdateStatusDisplay(
                    statName,
                    StatValueToPercentile(shipStats[statName].maxValue, value)
                    );
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        internal void RefreshStatDisplay()
        {
            foreach (KeyValuePair<ShipStatusName, ShipStatus> status in shipStats)
            {
                display.UpdateStatusDisplay(
                    status.Key,
                    StatValueToPercentile(status.Value.maxValue, status.Value.currentValue)
                    );
            }
        }

        internal float StatValueToPercentile(double maxValue, double currentValue)
        {
            return (float)(currentValue / maxValue);
        }

        public void Damage(int amount)
        {
            if (shipStats[ShipStatusName.Armor].currentValue < amount)
            {
                double remainingDamages = amount - shipStats[ShipStatusName.Armor].currentValue;
                UpdateStat(ShipStatusName.Armor, 0);
                UpdateStat(ShipStatusName.Health,
                    shipStats[ShipStatusName.Health].currentValue - remainingDamages);
            }
            else
            {
                UpdateStat(ShipStatusName.Armor, shipStats[ShipStatusName.Armor].currentValue - amount);
            }

            // TODO: Trigger defeat
        }

        public void ArmorUp()
        {
            if (shipStats[ShipStatusName.Armor].currentValue < shipStats[ShipStatusName.Armor].maxValue)
            {
                UpdateStat(ShipStatusName.Armor,
                    Math.Min(shipStats[ShipStatusName.Armor].maxValue, shipStats[ShipStatusName.Armor].currentValue + shipStats[ShipStatusName.Armor].upValue));
            }
        }

        // TODO: to implement
        public virtual void Repair(int amount) { }
    }
}
