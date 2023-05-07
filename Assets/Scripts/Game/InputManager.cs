using OfFogAndDust.Combat;
using OfFogAndDust.Ship;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfFogAndDust.Game
{
    internal class InputManager : MonoBehaviour
    {
        private static InputManager Instance;

        [SerializeField] private PlayerInput _input;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _input.actions["MoveCharacter"].performed += MoveCharacter;
            _input.actions["SelectCharacter"].performed += SelectCharacter;
        }

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
    }
}
