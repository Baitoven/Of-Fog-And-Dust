using Newtonsoft.Json;
using System;

namespace OfFogAndDust.Save.Types
{
    [Serializable]
    internal class TParsedSave
    {
        [JsonProperty("date")]
        internal DateTime Date { get; set; }

        // TODO
    }
}
