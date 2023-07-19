using Newtonsoft.Json;
using OfFogAndDust.Dialogue.Types;
using OfFogAndDust.Game;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OfFogAndDust.Dialogue.Parsing
{
    internal class DialogueParser
    {
        // TODO: implement routing (meaning remove this)
        private readonly string source = "default.json";

        internal void ParseDialogue() // TODO: add arg for routing
        {
            string sourceContent = File.ReadAllText(Path.Join(Application.streamingAssetsPath, source));
            List<TParsedDialogue> parsedDialogueList = JsonConvert.DeserializeObject<List<TParsedDialogue>>(sourceContent);
            foreach (TParsedDialogue parsedDialogue in parsedDialogueList) 
            {
                GameManager.Instance.dialogueBank.Add(parsedDialogue.id, new TDialogue(parsedDialogue));
            }
        }
    }
}
