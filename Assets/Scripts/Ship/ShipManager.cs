using OfFogAndDust.Combat;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Ship
{
    internal class ShipManager : MonoBehaviour
    {
        internal static ShipManager Instance;
        internal Character selectedCharacter { private set; get; }
        internal Ship ship;

        // TO REMOVE
        [SerializeField] private List<ShipTask> shipTaskList;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        // TO REMOVE
        public void Start()
        {
            ship = new Ship();
            ship.shipTasks = new Dictionary<Ship.ShipTaskName, ShipTask>();
            foreach (ShipTask task in shipTaskList)
            {
                ship.shipTasks.Add(task.taskName, task);
            }
        }

        internal void MoveCharacter(Vector2 destination)
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.Move(destination); 
            }
        }

        internal void SetSelectedCharacter(Character character)
        {
            selectedCharacter = character;
        }
    }
}
