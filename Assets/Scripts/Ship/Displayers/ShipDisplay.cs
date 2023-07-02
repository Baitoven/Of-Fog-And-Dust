using OfFogAndDust.Ship.Data;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Ship.Displayers
{
    internal class ShipDisplay : MonoBehaviour
    {
        [SerializeField] private List<TaskDisplay> tasks = new List<TaskDisplay>();
        private Dictionary<ShipTask.ShipTaskName, TaskDisplay> taskDisplay = new Dictionary<ShipTask.ShipTaskName, TaskDisplay>();

        private void Start()
        {
            foreach (TaskDisplay task in tasks)
            {
                taskDisplay.Add(task.shipTask, task);
            }
        }

        internal void UpdateDisplay(ShipTask.ShipTaskName shipTaskName, float value)
        {
            taskDisplay[shipTaskName].Set(value);
        }
    }
}
