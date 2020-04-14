namespace XamExchange.Models.FixerModels
{
    using Newtonsoft.Json;

    public class FixerError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }
    }
}
