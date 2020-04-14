namespace XamExchange.Models.FixerModels
{
    using Newtonsoft.Json;

    public class FixerResponseError : IFixerResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public FixerError Error { get; set; }

        public bool IsSuccessful() => this.Success;
    }
}
