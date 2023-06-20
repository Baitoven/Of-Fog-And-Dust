using OfFogAndDust.Combat;
using UnityEngine;

namespace OfFogAndDust.Ship
{
    internal class ShipManager : MonoBehaviour
    {
        internal static ShipManager Instance;
        internal Character selectedCharacter { private set; get; }

        [SerializeField] private AstarPath pathfinder;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void Start()
        {
            // rebuild ship navigation graph
            pathfinder.Scan();
        }

        // TEST FUNCTION
        //private void Update()
        //{
        //    Debug.Log(ship.IsTaskAssigned(Ship.ShipTaskName.Maneuver) + " " + ship.IsTaskAssigned(Ship.ShipTaskName.Weapons) + " " + ship.IsTaskAssigned(Ship.ShipTaskName.Repair));
        //}

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
