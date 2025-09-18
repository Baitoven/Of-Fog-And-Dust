using UnityEngine;
using UnityEngine.InputSystem;

namespace OfFogAndDust.Construction
{
    public class ConstructionManager : MonoBehaviour
    {
        public static ConstructionManager Instance;

        internal ConstructionPart selectedPart;
        [SerializeField] private ConstructionArea constructionArea;

        private void Awake()
        {
            Instance = this;
        }

        #region Construction Part Movement
        internal void SelectConstructionPart(ConstructionPart constructionPart)
        {
            selectedPart = constructionPart;
        }

        internal void ReleaseConstructionPart()
        {
            selectedPart = null;
        }

        private void FixedUpdate()
        {
            if (selectedPart != null)
            {
                Vector3 viewportPoint = Camera.main.ScreenToViewportPoint((Vector3)Mouse.current.position.ReadValue());
                selectedPart.transform.position = new Vector3(viewportPoint.x * Camera.main.pixelWidth, viewportPoint.y * Camera.main.pixelHeight, 0f);
            }
        }
        #endregion
    }
}

