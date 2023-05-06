using OfFogAndDust.Combat;
using UnityEngine;

namespace OfFogAndDust.Ship
{
    internal class ShipManager : MonoBehaviour
    {
        internal static ShipManager Instance;
        // TO REMOVE
        [SerializeField] internal Character test_character;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
