namespace XamExchange.Services
{
    using Newtonsoft.Json;
    using RestSharp;
    using XamExchange.Models.FixerModels;

    public class FixerDataStore
    {
        private const string BASE_URL = "http://data.fixer.io/api/";
        private const string ACCESS_KEY = "379c662b0df4eee19a9430b04fa0fa69";
        private readonly RestClient client;

        public FixerDataStore()
        {
            this.client = new RestClient(BASE_URL);
        }

        /// <summary>
        /// Chiama Fixer per farsi restituire l'elenco di tutti gli ultimi tassi di conversione.
        /// </summary>
        /// <returns>
        /// Elenco degli ultimi tassi di conversione.
        /// Viene provvisto anche la data di aggiornamento in formato timestamp.
        /// </returns>
        public IFixerResponse GetLatestCurrencyExchange()
        {
            var request = this.DecorateRequest("latest");
            var response = this.client.Execute<Currency>(request);
            if (response.IsSuccessful)
                return response.Data;
            return JsonConvert.DeserializeObject<FixerResponseError>(response.Content);
            /*var response = await this.client.GetAsync($"latest&access_key={this.accessKey}");
            var contentString = response.Content.ReadAsStringAsync();
            IFixerResponse latest;
            contentString.Wait();
            if (response.IsSuccessStatusCode)
                latest = JsonConvert.DeserializeObject<Currency>(contentString.Result);
            else
                latest = JsonConvert.DeserializeObject<FixerResponseError>(contentString.Result);
            return latest;
            */
        }

        /// <summary>
        /// Chiama Fixer per farsi restituire l'elenco di tutti i possibili tassi di conversione.
        /// </summary>
        /// <returns>Elenco dei simboli e nomi delle valute convertibili.</returns>
        public IFixerResponse GetAllCurrencySymbols()
        {
            var request = this.DecorateRequest("symbols");
            var response = this.client.Execute<AllSymbols>(request);
            if (response.IsSuccessful)
                return response.Data;
            return JsonConvert.DeserializeObject<FixerResponseError>(response.Content);
            /*var response = await this.client.GetAsync($"symbols&access_key={this.accessKey}");
            var contentString = response.Content.ReadAsStringAsync();
            contentString.Wait();
            var symbols = JsonConvert.DeserializeObject<Symbols>(contentString.Result);
            return symbols;*/
        }

        /// <summary>
        /// Add the default access key to a generic request.
        /// </summary>
        /// <param name="root">Function to call on Fixer.io</param>
        /// <returns>Base request to Fixer.io</returns>
        private RestRequest DecorateRequest(string function)
        {
            var request = new RestRequest(function, Method.GET, DataFormat.Json);
            request.AddParameter("access_key", ACCESS_KEY);
            return request;
        }

    }
}
