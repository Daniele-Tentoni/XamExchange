namespace XamExchange.Models
{
    using Newtonsoft.Json;

    public class FixerResponseError : FixerResponse
    {
        [JsonProperty("error")]
        public FixerError Error { get; set; }
    }
}
