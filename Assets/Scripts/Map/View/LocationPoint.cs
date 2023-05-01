using OfFogAndDust.Company;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Map
{
    [RequireComponent(typeof(Button))]
    public class LocationPoint : MonoBehaviour
    {
        public Image image;
        public Button button;

        private bool isEnabled = false;

        private void OnButtonClicked()
        {
            CompanyManager.Instance.Move(this);
        }

        public void SetEnable()
        {
            if (!isEnabled)
            {
                button.onClick.AddListener(OnButtonClicked);
                isEnabled = true;
            }  
        }

        public void SetDisable()
        {
            if (isEnabled)
            {
                button.onClick.RemoveListener(OnButtonClicked);
                isEnabled = false;
            }
        }
    }
}

