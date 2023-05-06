using OfFogAndDust.Ship;
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
        }

        private void MoveCharacter(InputAction.CallbackContext context)
        {
            Debug.Log(Mouse.current.position.ReadValue()); //Camera.main.ScreenToWorldPoint(
            ShipManager.Instance.test_character.Move(Mouse.current.position.ReadValue());
        }
    }
}
