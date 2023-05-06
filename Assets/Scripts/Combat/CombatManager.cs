using UnityEngine;

namespace OfFogAndDust.Combat
{
    internal class CombatManager : MonoBehaviour
    {
        internal enum CombatState
        {
            Paused,
            Live
        }

        private float deltaTime = Time.fixedDeltaTime;
        internal CombatState currentCombatState;

        private void FixedUpdate()
        {
            if (currentCombatState == CombatState.Live)
            {

            }
        }

        private void Pause()
        {
            currentCombatState = CombatState.Paused;
        }
    }
}
