using OfFogAndDust.Game;
using OfFogAndDust.Town.Building.Page;
using OfFogAndDust.Town.Displayers;
using OfFogAndDust.Town.Page.Base;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Town
{
    internal class TownManager : MonoBehaviour
    {
        public static TownManager Instance;
        [SerializeField] private List<BuildingDisplay> buildingDisplayers;
        [SerializeField] private List<BuildingPageBase> buildingPages;

        internal bool isPageOpened = false;

        private void Awake()
        {
            Instance = this;
        }

        internal void SelectBuilding(BuildingDisplay building)
        {
            if (!isPageOpened)
            {
                buildingPages.Find((BuildingPageBase b) => b.buildingName == building.buildingName).gameObject.SetActive(true);
                isPageOpened = true;
            }
        }
    }
}
