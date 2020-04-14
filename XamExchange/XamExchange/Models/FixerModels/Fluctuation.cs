using Newtonsoft.Json;

namespace XamExchange.Models.FixerModels
{
    public class Fluctuation
    {
        [JsonProperty("start_rate")]
        public double StartRate { get; set; }

        [JsonProperty("end_rate")]
        public double EndRate { get; set; }

        [JsonProperty("change")]
        public double Change { get; set; }

        [JsonProperty("change_pct")]
        public double ChangePercentile { get; set; }
    }
}
