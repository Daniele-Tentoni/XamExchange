namespace XamExchange.Services
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using XamExchange.Models;

    public class FixerDataStore
    {
        private readonly string accessKey = "379c662b0df4eee19a9430b04fa0fa69";
        private readonly HttpClient client;

        public FixerDataStore()
        {
            this.client = new HttpClient()
            {
                BaseAddress = new Uri("http://data.fixer.io/api/")
            };
        }

        public async Task<IFixerResponse> GetLatestCurrencyExchange()
        {
            var response = await this.client.GetAsync($"latest&access_key={this.accessKey}");
            var contentString = response.Content.ReadAsStringAsync();
            contentString.Wait();
            IFixerResponse latest;
            if (response.IsSuccessStatusCode)
                latest = JsonConvert.DeserializeObject<Currency>(contentString.Result);
            else
                latest = JsonConvert.DeserializeObject<FixerResponseError>(contentString.Result);
            return latest;
        }

        public async Task<IFixerResponse> GetAllCurrencySymbols()
        {
            var response = await this.client.GetAsync($"symbols&access_key={this.accessKey}");
            var contentString = response.Content.ReadAsStringAsync();
            contentString.Wait();
            var symbols = JsonConvert.DeserializeObject<Symbols>(contentString.Result);
            return symbols;
        }

    }
}
