using OfFogAndDust.Game;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Town
{
    internal class TownManager : MonoBehaviour
    {
        public static TownManager Instance;

        public Button _launchExpeditionButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _launchExpeditionButton.onClick.AddListener(GameManager.Instance.LaunchExpedition);
        }
    }
}
