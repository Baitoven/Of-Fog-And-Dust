using System;

namespace OfFogAndDust.Dialogue.Types
{
    [Serializable]
    internal class TDialogue
    {
        internal string background;
        internal string speakerImage;
        internal string speakerName;
        internal string text;

        internal TDialogue() { }

        internal TDialogue(TParsedDialogue parsedDialogue)
        {
            background = parsedDialogue.background;
            speakerImage = parsedDialogue.speakerImage;
            speakerName = parsedDialogue.speakerName;
            text = parsedDialogue.text;
        }
    }
}
