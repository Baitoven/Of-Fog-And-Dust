using OfFogAndDust.Combat;
using OfFogAndDust.Ship.Data;
using OfFogAndDust.Ship.Interface;
using System.Collections.Generic;
using UnityEngine;
using static OfFogAndDust.Ship.Data.ShipTask;

namespace OfFogAndDust.Ship.Base
{
    internal class ShipBase : MonoBehaviour, IShip
    {
        internal Dictionary<ShipTaskName, ShipTask> shipTasks;

        [SerializeField] List<ShipTask> shipTaskList;

        internal void Start()
        {
            shipTasks = new Dictionary<ShipTaskName, ShipTask>();
            foreach (ShipTask task in shipTaskList)
            {
                shipTasks.Add(task.taskName, task);
            }
        }

        public bool IsTaskAssigned(ShipTaskName task)
        {
            if (shipTasks.ContainsKey(task))
            {
                return shipTasks[task].assigned != null;
            }
            else
            {
                return false;
            }
        }

        // at this point, shipTask should contain the task "task"
        public void AssignCharacterToTask(ShipTaskName task, Character character)
        {
            shipTasks[task].assigned = character;
        }
    }
}
