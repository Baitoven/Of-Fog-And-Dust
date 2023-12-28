using OfFogAndDust.Dialogue.Displayers;
using UnityEngine;

namespace OfFogAndDust.Dialogue
{
    internal class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;
        [SerializeField] private DialogueDisplay display;

        private void Awake()
        {
            Instance = this;
        }
    }
}
