using OfFogAndDust.Dialogue.Displayers;
using OfFogAndDust.Dialogue.Types;
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

        // TODO: get dialogue from GameManager.Instance.dialogueBank
        // should not exist like this
        internal TDialogue GetDialogue(string id)
        {
            return new TDialogue();
        }
    }
}
