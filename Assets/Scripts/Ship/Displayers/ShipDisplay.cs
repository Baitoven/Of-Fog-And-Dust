using Assets.Scripts.Ship.Data;
using OfFogAndDust.Ship.Data;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Ship.Displayers
{
    internal class ShipDisplay : MonoBehaviour
    {
        [SerializeField] private List<TaskDisplay> tasks = new List<TaskDisplay>();
        [SerializeField] private List<StatusDisplay> status = new List<StatusDisplay>();
        private Dictionary<ShipTask.ShipTaskName, TaskDisplay> taskDisplay = new Dictionary<ShipTask.ShipTaskName, TaskDisplay>();
        private Dictionary<ShipStatus.ShipStatusName, StatusDisplay> statusDisplay = new Dictionary<ShipStatus.ShipStatusName, StatusDisplay>();

        internal void NStart()
        {
            foreach (TaskDisplay task in tasks)
            {
                taskDisplay.Add(task.shipTask, task);
            }

            foreach (StatusDisplay statu in status)
            {
                statusDisplay.Add(statu.shipStatus, statu);
            }
        }

        internal void UpdateTaskDisplay(ShipTask.ShipTaskName shipTaskName, float value)
        {
            taskDisplay[shipTaskName].Set(value);
        }

        internal void UpdateStatusDisplay(ShipStatus.ShipStatusName shipStatusName, float value)
        {
            statusDisplay[shipStatusName].Set(value);
        }
    }
}
