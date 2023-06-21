using OfFogAndDust.Combat;
using TMPro;
using UnityEngine;
using static OfFogAndDust.Combat.CombatManager;

namespace OfFogAndDust.Utils
{
    internal class Clock : MonoBehaviour
    {
        public float CurrentTime { get; private set; }

        [SerializeField] private TextMeshProUGUI clockDisplayText;

        private void Update()
        {
            if (CombatManager.Instance.state == CombatState.Running)
            {
                CurrentTime += Time.deltaTime;
                clockDisplayText.text = CurrentTime.ToString();
            }
        }

        public void ResetClock()
        {
            CurrentTime = 0;
        }
    }
}
