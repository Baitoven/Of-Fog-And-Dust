using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Menu
{
    public class SettingsMenu : MonoBehaviour
    {
        public enum SettingsMenuState
        {
            Close,
            Open
        }

        internal SettingsMenuState state = SettingsMenuState.Close;

        [SerializeField] internal Button closeMenu;
        [SerializeField] internal Button closeGame;

        private void Start()
        {
            closeMenu.onClick.AddListener(OpenMenu);
            closeGame.onClick.AddListener(CloseGame);
        }

        private void OnDestroy()
        {
            closeMenu.onClick.RemoveAllListeners();
            closeGame.onClick.RemoveAllListeners();
        }

        public void OpenMenu()
        {
            if (state == SettingsMenuState.Close)
            {
                state = SettingsMenuState.Open;
                gameObject.SetActive(true);
            }
            else
            {
                state = SettingsMenuState.Close;
                gameObject.SetActive(false);
            }
        }

        private void CloseGame()
        {
            Application.Quit();
        }
    }
}


