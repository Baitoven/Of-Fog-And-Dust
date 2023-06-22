using OfFogAndDust.Combat.CombatEvent;
using OfFogAndDust.Utils;
using System;
using TMPro;
using UnityEngine;

namespace OfFogAndDust.Combat
{
    internal class CombatManager : MonoBehaviour
    {
        internal enum CombatState
        {
            Running,
            Paused,
            Idle
        }

        internal static CombatManager Instance;
        internal CombatState state;

        [SerializeField] internal Clock clock;
        internal CombatEventManager eventManager;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);

            eventManager = new CombatEventManager();
        }

        private void Start()
        {
            StartCombat();

            // FOR TEST PURPOSES
            eventManager.AddEvent(new ShipAttackEvent
            {
                duration = 8f
            });
        }

        private void StartCombat()
        {
            state = CombatState.Running;
        }

        internal void Pause()
        {
            state = (state == CombatState.Running ? CombatState.Paused : CombatState.Running);
        }
    }
}
