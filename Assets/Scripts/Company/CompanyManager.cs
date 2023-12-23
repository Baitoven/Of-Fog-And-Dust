using OfFogAndDust.Map;
using UnityEngine;

namespace OfFogAndDust.Company
{
    public class CompanyManager : MonoBehaviour
    {
        public static CompanyManager Instance;
        public LocationPoint location;

        private void Awake()
        {
            Instance = this;
        }

        public void Move(LocationPoint newLocation)
        {
            location = newLocation;
            MapManager.Instance.Refresh();
        }
    }
}

