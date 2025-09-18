using OfFogAndDust.Map;
using UnityEngine;

namespace OfFogAndDust.Company
{
    public class CompanyManager : MonoBehaviour
    {
        public static CompanyManager Instance;
        public int mapLocation;

        private void Awake()
        {
            Instance = this;
        }

        public void Move(LocationPoint newLocation)
        {
            mapLocation = newLocation.pointNumber;
            MapManager.Instance.Refresh();
        }
    }
}

