using OfFogAndDust.Game;
using OfFogAndDust.Town;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Menu
{
    internal class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance;
        public Button _startGameButton;
        public Button _leaveGameButton;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _startGameButton.onClick.AddListener(GameManager.Instance.LaunchGame);
            // TODO : _leaveGameButton.onClick.AddListener(GameManager.Instance.LaunchExpedition);
        }
    }
}
