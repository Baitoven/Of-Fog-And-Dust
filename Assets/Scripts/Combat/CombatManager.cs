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

        [SerializeField] private Clock clock;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

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
    }
}
