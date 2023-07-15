using Assets.Scripts.Ship.Ships;
using OfFogAndDust.Combat;
using OfFogAndDust.Ship.Data;
using OfFogAndDust.Ship.Ships;
using System;
using UnityEngine;

namespace OfFogAndDust.Ship
{
    internal class ShipManager : MonoBehaviour
    {
        internal static ShipManager Instance;

        // TODO: change this so ships are auto assigned
        [SerializeField] internal CompanyShip companyShip;
        [SerializeField] internal DefaultEnemyShip enemyShip;

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

        internal void TriggerEffect(ShipTask.ShipTaskName taskName, bool isEnemy, int value = 0)
        {
            switch (taskName, isEnemy)
            {
                case (ShipTask.ShipTaskName.Weapons, true):
                    companyShip.Damage(value);
                    break;
                case (ShipTask.ShipTaskName.Weapons, false):
                    enemyShip.Damage(value);
                    break;
                case (ShipTask.ShipTaskName.Repair, true):
                    enemyShip.ArmorUp(); 
                    break;
                case (ShipTask.ShipTaskName.Repair, false):
                    companyShip.ArmorUp();
                    break;
                default:
                    break;
            }
        }
    }
}
