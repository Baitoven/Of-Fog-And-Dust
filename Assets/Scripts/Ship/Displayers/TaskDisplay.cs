using OfFogAndDust.Ship.Data;
using UnityEngine;

namespace OfFogAndDust.Ship.Displayers
{
    internal class TaskDisplay : MonoBehaviour
    {
        [SerializeField] internal ShipTask.ShipTaskName shipTask;
        [SerializeField] private Transform imageGameObjet;

        internal void Set(float value)
        {
            imageGameObjet.localScale = new Vector3(value, 1f, 1f);
        }
    }
}
