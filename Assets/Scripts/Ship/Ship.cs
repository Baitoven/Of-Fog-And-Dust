using OfFogAndDust.Combat;
using System.Collections.Generic;

namespace OfFogAndDust.Ship
{
    internal class Ship
    {
        internal enum ShipTaskName
        {
            Maneuver,
            Repair,
            Weapons
        }

        internal Dictionary<ShipTaskName, ShipTask> shipTasks;

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
