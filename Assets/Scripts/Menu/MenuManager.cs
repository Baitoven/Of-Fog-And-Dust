using UnityEngine;

namespace OfFogAndDust.Menu
{
    internal class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        [SerializeField] private SettingsMenu settingsMenu;

        private void Awake()
        {
            Instance = this;
        }

        public void OpenSettingsMenu() => settingsMenu.OpenMenu();
    }
}
