namespace XamExchange
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using XamExchange.Models;
    using XamExchange.Models.FixerModels;
    using XamExchange.Services;
    using XamExchange.Views;

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<CurrencyDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static async Task LoadFixerData()
        {
            var client = new FixerDataStore();
            var symbols = client.GetAllCurrencySymbols();
            var currencies = client.GetLatestCurrencyExchange();
            await Task.WhenAll(symbols, currencies);
            var currencyDataStore = DependencyService.Get<CurrencyDataStore>();

            if (symbols.Result.IsSuccessful() && currencies.Result.IsSuccessful())
            {
                var fsymbols = (AllSymbols)symbols.Result;
                var frates = (Currency)currencies.Result;
                var lastUpdate = DateTimeOffset.FromUnixTimeSeconds(frates.Timestamp).LocalDateTime;
                Preferences.Set("last_update", lastUpdate);
                var tasks = new List<Task>();
                foreach (var symbol in fsymbols.Symbols)
                {
                    tasks.Add(currencyDataStore.AddOrUpdateItemAsync(new CompleteCurrency
                    {
                        Code = symbol.Key,
                        Name = symbol.Value,
                        Rate = (double)frates.Rates[symbol.Key]
                    }));
                }
                await Task.WhenAll(tasks);
            }
        }
    }
}
