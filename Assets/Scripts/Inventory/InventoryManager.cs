using UnityEngine;

namespace OfFogAndDust.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public InventoryManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
