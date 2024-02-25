using Newtonsoft.Json;
using OfFogAndDust.Map.Types;
using System;

namespace OfFogAndDust.Save.Types
{
    [Serializable]
    internal class TParsedSave
    {
        [JsonProperty("date")]
        internal DateTime date { get; set; }

        [JsonProperty("map")]
        internal TLinearMap map { get; set; }

        // TODO
    }
}
