using Assets.Scripts.Ship.Data;
using UnityEngine;

namespace OfFogAndDust.Ship.Displayers
{
    internal class StatusDisplay : MonoBehaviour
    {
        [SerializeField] internal ShipStatus.ShipStatusName shipStatus;
        [SerializeField] private Transform imageGameObjet;

        internal void Set(float value)
        {
            imageGameObjet.localScale = new Vector3(value, 1f, 1f);
        }
    }
}
