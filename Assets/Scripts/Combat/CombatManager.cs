using OfFogAndDust.Combat.CombatEvent;
using OfFogAndDust.Combat.CombatEvent.Base;
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

        // TO REFRACTOR: remove this because scene is supposed to be launched via map
        private void Start()
        {
            StartCombat();
        }

        private void StartCombat()
        {
            state = CombatState.Running;
        }

        internal void Pause()
        {
            state = (state == CombatState.Running ? CombatState.Paused : CombatState.Running);
        }

        internal void AddOrResumeEvent<T>(T newEvent) where T : CombatEventBase => eventManager.AddOrResumeEvent<T>(newEvent);

        // TO IMPROVE
        internal void DelayEvent(Type eventType) => eventManager.DelayEvent(new CombatEventManager.FindEventSearch { type = eventType }, clock.CurrentTime);
    }
}
