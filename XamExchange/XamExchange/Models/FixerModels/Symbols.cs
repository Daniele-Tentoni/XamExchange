using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamExchange.Models
{
    public class Symbols: IFixerResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "symbols")]
        public IDictionary<string, string> SymbolDictionary { get; set; }

        public bool IsSuccessful() => this.Success;
    }
}
