﻿using Newtonsoft.Json;
using System;

namespace OfFogAndDust.Dialogue.Types
{
    [Serializable]
    internal class TParsedDialogue
    {
        [JsonProperty("id")]
        internal int id { get; set; }

        [JsonProperty("background")]
        internal string background { get; set; }

        [JsonProperty("speakerImage")]
        internal string speakerImage { get; set; }

        [JsonProperty("speakerName")]
        internal string speakerName { get; set; }

        [JsonProperty("text")]
        internal string text { get; set; }
    }
}
