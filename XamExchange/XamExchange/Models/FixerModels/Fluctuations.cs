using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamExchange.Models.FixerModels
{
    public class Fluctuations: IFixerResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, Fluctuation> Rates { get; set; }

        public bool IsSuccessful() => this.Success;
    }
}
