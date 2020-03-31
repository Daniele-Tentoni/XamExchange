using Newtonsoft.Json;
using System.Collections.Generic;

namespace XamExchange.Models
{
    public class Rates
    {
        [JsonProperty(PropertyName = "rates")]
        public List<Rate> RateList { get; set; }
    }
}
