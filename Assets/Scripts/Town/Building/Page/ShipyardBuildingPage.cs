using OfFogAndDust.Game;
using OfFogAndDust.Town.Page.Base;
using UnityEngine.UI;

namespace OfFogAndDust.Town.Building.Page
{
    internal class ShipyardBuildingPage : BuildingPageBase
    {
        public Button _launchConstructionButton;

        internal override void Start()
        {
            base.Start();
            _launchConstructionButton.onClick.AddListener(GameManager.Instance.LaunchConstruction);
        }
    }
}
