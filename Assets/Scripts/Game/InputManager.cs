using OfFogAndDust.Combat;
using OfFogAndDust.Ship;
using OfFogAndDust.Town;
using OfFogAndDust.Town.Displayers;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static OfFogAndDust.Game.GameManager;

namespace OfFogAndDust.Game
{
    internal class InputManager : MonoBehaviour
    {
        private static InputManager Instance;

        [SerializeField] private PlayerInput _input;

        #region InputSwitching
        internal void SwitchInputMap(GameState newGameState)
        {
            _input.SwitchCurrentActionMap(GameStateToInputActionMapText(newGameState));
        }

        private string GameStateToInputActionMapText(GameState gameState) => gameState switch
        {
            GameState.Town => "Town",
            GameState.Expedition => "Expedition",
            GameState.Combat => "Combat",
            _ => throw new ArgumentOutOfRangeException(),
        };
        #endregion

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _input.actions["MoveCharacter"].performed += MoveCharacter;
            _input.actions["SelectCharacter"].performed += SelectCharacter;
            _input.actions["PauseCombat"].performed += PauseCombat;
            _input.actions["SelectBuilding"].performed += SelectBuilding;
        }

        #region Combat
        private void MoveCharacter(InputAction.CallbackContext _)
        {
            ShipManager.Instance.MoveCharacter(Mouse.current.position.ReadValue());
        }

        private void SelectCharacter(InputAction.CallbackContext _)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector3)Mouse.current.position.ReadValue(), Vector3.forward);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out Character character))
                {
                    ShipManager.Instance.SetSelectedCharacter(character);
                } 
                else
                {
                    ShipManager.Instance.SetSelectedCharacter(null);
                }
            }
        }

        private void PauseCombat(InputAction.CallbackContext _)
        {
            CombatManager.Instance.Pause();
        }
        #endregion

        #region Town
        private void SelectBuilding(InputAction.CallbackContext _)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector3)Mouse.current.position.ReadValue(), Vector3.forward);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out BuildingDisplay building))
                {
                    TownManager.Instance.SelectBuilding(building);
                }
            }
        }
        #endregion

    }
}
