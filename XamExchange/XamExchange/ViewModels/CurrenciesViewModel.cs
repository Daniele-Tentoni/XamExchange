namespace XamExchange.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    using XamExchange.Models;
    using XamExchange.Models.FixerModels;
    using XamExchange.Services;
    using XamExchange.Views;

    public class CurrenciesViewModel : BaseViewModel
    {
        public ObservableCollection<CompleteCurrency> Currencies { get; set; }
        public Command LoadCurrenciesCommand { get; set; }

        public CurrenciesViewModel()
        {
            this.Title = "Currencies";
            this.Currencies = new ObservableCollection<CompleteCurrency>();
            this.LoadCurrenciesCommand = new Command(async () => await this.ExecuteLoadCurrenciesCommand());

            MessagingCenter.Subscribe<NewItemPage, CompleteCurrency>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as CompleteCurrency;
                this.Currencies.Add(newItem);
                _ = await this.DataStore.AddOrUpdateItemAsync(newItem);
            });
        }

        async Task ExecuteLoadCurrenciesCommand()
        {
            if (this.IsBusy)
                return;

            this.IsBusy = true;

            try
            {
                this.Currencies.Clear();
                var fixer = new FixerDataStore();
                var symbols =  fixer.GetAllCurrencySymbols();
                var rates =  fixer.GetLatestCurrencyExchange();
                if (symbols.IsSuccessful() && rates.IsSuccessful())
                {
                    var fsymbols = (AllSymbols)symbols;
                    var frates = (Currency)rates;
                    foreach (var symbol in fsymbols.Symbols)
                    {
                        this.Currencies.Add(new CompleteCurrency
                        {
                            Code = symbol.Key,
                            Name = symbol.Value,
                            Rate = (double)frates.Rates[symbol.Key]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}