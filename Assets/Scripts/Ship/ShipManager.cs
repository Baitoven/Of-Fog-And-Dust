using OfFogAndDust.Combat;
using UnityEngine;

namespace OfFogAndDust.Ship
{
    internal class ShipManager : MonoBehaviour
    {
        internal static ShipManager Instance;
        internal Character selectedCharacter { private set; get; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        internal void MoveCharacter(Vector2 destination)
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.Move(destination); 
            }
        }

        internal void SetSelectedCharacter(Character character)
        {
            selectedCharacter = character;
        }
    }
}
