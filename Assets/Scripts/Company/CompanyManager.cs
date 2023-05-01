using UnityEngine;

namespace OfFogAndDust.Company
{
    public class CompanyManager : MonoBehaviour
    {
        public static CompanyManager Instance;
        public GameObject location;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }


    }
}

