using System.Collections.Generic;
using System;

namespace OfFogAndDust.Dialogue.Types
{
    [Serializable]
    internal class TChoice
    {
        internal string background;
        internal string speakerImage;
        internal string speakerName;
        internal string text;
        internal List<string> answers;

        internal TChoice() { }

        internal TChoice(TParsedChoice parsedChoice)
        {
            background = parsedChoice.background;
            speakerImage = parsedChoice.speakerImage;
            speakerName = parsedChoice.speakerName;
            text = parsedChoice.question;
            answers = new List<string>();
            if (parsedChoice.answers1 != null)
                answers.Add(parsedChoice.answers1);
            if (parsedChoice.answers2 != null)
                answers.Add(parsedChoice.answers2);
            if (parsedChoice.answers3 != null)
                answers.Add(parsedChoice.answers3);
        }
    }
}
