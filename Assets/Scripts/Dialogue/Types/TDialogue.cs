using System;
using System.Collections.Generic;

namespace OfFogAndDust.Dialogue.Types
{
    [Serializable]
    internal class TDialogue
    {
        internal string background;
        internal string speakerImage;
        internal string speakerName;
        internal string text;
        internal List<string> answers;

        internal TDialogue() { }

        internal TDialogue(TParsedDialogue parsedDialogue)
        {
            background = parsedDialogue.background;
            speakerImage = parsedDialogue.speakerImage;
            speakerName = parsedDialogue.speakerName;
            text = parsedDialogue.text;
            answers = new List<string>();
            if (parsedDialogue.answers1 != null)
                answers.Add(parsedDialogue.answers1);
            if (parsedDialogue.answers2 != null)
                answers.Add(parsedDialogue.answers2);
            if (parsedDialogue.answers3 != null)
                answers.Add(parsedDialogue.answers3);
        }
    }
}
