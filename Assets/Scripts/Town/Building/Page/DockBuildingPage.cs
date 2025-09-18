using OfFogAndDust.Game;
using OfFogAndDust.Town.Page.Base;
using UnityEngine.UI;

namespace OfFogAndDust.Town.Building.Page
{
    internal class DockBuildingPage : BuildingPageBase
    {
        public Button _launchExpeditionButton;

        internal override void Start()
        {
            base.Start();
            _launchExpeditionButton.onClick.AddListener(GameManager.Instance.LaunchExpedition);
        }
    }
}
