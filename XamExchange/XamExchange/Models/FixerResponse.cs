namespace XamExchange.Models
{
    using Newtonsoft.Json;

    public class FixerResponse: IFixerResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }
}
