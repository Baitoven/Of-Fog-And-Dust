using Newtonsoft.Json;
using System;

namespace OfFogAndDust.Dialogue.Types
{
    [Serializable]
    internal class TParsedChoice
    {
        [JsonProperty("id")]
        internal int id { get; set; }

        [JsonProperty("background")]
        internal string background { get; set; }

        [JsonProperty("speakerImage")]
        internal string speakerImage { get; set; }

        [JsonProperty("speakerName")]
        internal string speakerName { get; set; }

        [JsonProperty("question")]
        internal string question { get; set; }

        [JsonProperty("answers1")]
        internal string answers1 { get; set; }

        [JsonProperty("answers2")]
        internal string answers2 { get; set; }

        [JsonProperty("answers3")]
        internal string answers3 { get; set; }
    }
}
