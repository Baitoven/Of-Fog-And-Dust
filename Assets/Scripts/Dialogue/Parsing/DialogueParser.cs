using Newtonsoft.Json;
using OfFogAndDust.Dialogue.Types;
using OfFogAndDust.Game;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OfFogAndDust.Dialogue.Parsing
{
    internal class ContentParser
    {
        // TODO: implement routing (meaning remove this)
        private readonly string dialogueSource = "dialogue.json";
        private readonly string choiceSource = "choice.json";

        internal void ParseDialogue() // TODO: add arg for routing
        {
            string sourceContent = File.ReadAllText(Path.Join(Application.streamingAssetsPath, dialogueSource));
            List<TParsedDialogue> parsedDialogueList = JsonConvert.DeserializeObject<List<TParsedDialogue>>(sourceContent);
            foreach (TParsedDialogue parsedDialogue in parsedDialogueList) 
            {
                GameManager.Instance.dialogueBank.Add(parsedDialogue.id, new TDialogue(parsedDialogue));
            }
        }

        internal void ParseChoice()
        {
            string sourceContent = File.ReadAllText(Path.Join(Application.streamingAssetsPath, choiceSource));
            List<TParsedChoice> parsedChoiceList = JsonConvert.DeserializeObject<List<TParsedChoice>>(sourceContent);
            foreach (TParsedChoice parsedChoice in parsedChoiceList)
            {
                GameManager.Instance.choiceBank.Add(parsedChoice.id, new TChoice(parsedChoice));
            }
        }
    }
}
