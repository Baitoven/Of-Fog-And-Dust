using OfFogAndDust.Game;
using OfFogAndDust.Town.Displayers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Town
{
    internal class TownManager : MonoBehaviour
    {
        public static TownManager Instance;
        [SerializeField] private List<BuildingDisplay> buildingDisplayers;

        public Button _launchExpeditionButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _launchExpeditionButton.onClick.AddListener(GameManager.Instance.LaunchExpedition);
        }

        internal void SelectBuilding(BuildingDisplay building)
        {
            // TODO: implement this so the function activates the building page
            //building.buildingName
        }
    }
}
