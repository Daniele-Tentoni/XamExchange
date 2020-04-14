using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamExchange.Models.FixerModels
{
    public class AllSymbols: IFixerResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "symbols")]
        public Dictionary<string, string> Symbols { get; set; }

        public bool IsSuccessful() => this.Success;
    }
}
