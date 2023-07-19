using Newtonsoft.Json;
using System;

namespace OfFogAndDust.Dialogue.Types
{
    [Serializable]
    internal class TParsedDialogue
    {
        [JsonProperty("id")]
        internal int id;

        [JsonProperty("background")]
        internal string background;

        [JsonProperty("speakerImage")]
        internal string speakerImage;

        [JsonProperty("speakerName")]
        internal string speakerName;

        [JsonProperty("text")]
        internal string text;

        [JsonProperty("answers1")]
        internal string answers1;

        [JsonProperty("answers2")]
        internal string answers2;

        [JsonProperty("answers3")]
        internal string answers3;
    }
}
