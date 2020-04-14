namespace XamExchange.Models.FixerModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Currency : IFixerResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty(PropertyName = "base")]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "rates")]
        public Dictionary<string, decimal> Rates { get; set; }

        public bool IsSuccessful() => this.Success;
    }
}
