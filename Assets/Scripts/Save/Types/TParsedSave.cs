using Newtonsoft.Json;
using System;

namespace OfFogAndDust.Save.Types
{
    [Serializable]
    internal class TParsedSave
    {
        [JsonProperty("date")]
        internal DateTime date { get; set; }

        [JsonProperty("map")]
        internal Map.Map map { get; set; }

        // TODO
    }
}
