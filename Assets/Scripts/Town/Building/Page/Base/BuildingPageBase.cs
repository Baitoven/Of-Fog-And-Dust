using UnityEngine;
using UnityEngine.UI;
using static OfFogAndDust.Town.Building.Building;

namespace OfFogAndDust.Town.Page.Base
{
    internal class BuildingPageBase : MonoBehaviour
    {
        [SerializeField] internal BuildingName buildingName;
        [SerializeField] private Button closeButton;

        private void Start()
        {
            closeButton.onClick.AddListener(OnClose);
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }

        private void OnClose()
        {
            this.gameObject.SetActive(false);
            TownManager.Instance.isPageOpened = false;
        }
    }
}
