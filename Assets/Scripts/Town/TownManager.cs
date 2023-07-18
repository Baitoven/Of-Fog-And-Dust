using OfFogAndDust.Game;
using OfFogAndDust.Town.Building.Page;
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
        [SerializeField] private List<BuildingPage> buildingPages;

        public Button _launchExpeditionButton;

        internal bool isPageOpened = false;

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
            if (!isPageOpened)
            {
                buildingPages.Find((BuildingPage b) => b.buildingName == building.buildingName).gameObject.SetActive(true);
                isPageOpened = true;
            }
        }
    }
}
