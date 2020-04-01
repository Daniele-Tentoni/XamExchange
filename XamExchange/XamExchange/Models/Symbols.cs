using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamExchange.Models
{
    public class Symbols: FixerResponse
    {
        [JsonProperty(PropertyName = "Symbols")]
        public IDictionary<string, string> SymbolDictionary { get; set; }
    }
}
