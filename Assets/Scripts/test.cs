using OfFogAndDust.Utils;
using System.Collections;
using UnityEngine;

namespace OfFogAndDust
{
    internal class test : MonoBehaviour
    {
        private void DelayedStart()
        {
            Timer timer = new Timer(5f);
            timer.OnTimerStart.AddListener(() => Debug.Log("timer started"));
            timer.OnTimerEnd.AddListener(() => Debug.Log("timer finished"));
            timer.StartTimer();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3);
            DelayedStart();
        }
    }
}
