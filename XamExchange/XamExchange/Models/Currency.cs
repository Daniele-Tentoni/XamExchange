namespace XamExchange.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Currency : IFixerResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty(PropertyName = "base")]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "rates")]
        public IDictionary<string, decimal> Rates { get; set; }

        public bool IsSuccessful() => this.Success;
    }
}
