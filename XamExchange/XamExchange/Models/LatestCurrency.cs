namespace XamExchange.Models
{
    using Newtonsoft.Json;

    public class LatestCurrency : FixerResponse
    {
        [JsonProperty(PropertyName = "timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty(PropertyName = "base")]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "rates")]
        public Rates Rates { get; set; }
    }
}
