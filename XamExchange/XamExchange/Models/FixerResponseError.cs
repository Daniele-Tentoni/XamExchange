namespace XamExchange.Models
{
    using Newtonsoft.Json;

    public class FixerResponseError : IFixerResponse
    {
        [JsonProperty("error")]
        public FixerError Error { get; set; }

        public bool IsSuccessful() => false;
    }
}
