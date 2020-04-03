namespace XamExchange.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XamExchange.Models;
    using XamExchange.Services;

    public class ExchangeViewModel : BaseViewModel
    {
        public List<CompleteCurrency> Currencies;

        public ObservableCollection<string> PickerCurrencies => 
            new ObservableCollection<string>(
                this.Currencies.Select(
                    s => s.Code + ": " + s.Name));

        private int _fromSelected;
        public int FromSelected
        {
            get => this._fromSelected;
            set => this.SetProperty(ref this._fromSelected, value);
        }
        private int _toSelected;
        public int ToSelected
        {
            get => this._toSelected;
            set => this.SetProperty(ref this._toSelected, value);
        }

        private string _fromText;
        public string FromText
        {
            get => this._fromText;
            set => this.SetProperty(ref this._fromText, value);
        }
        private string _toText;
        public string ToText
        {
            get => this._toText;
            set => this.SetProperty(ref this._toText, value);
        }

        public Command LoadCurrenciesCommand { get; set; }

        public Command ExchangeCurrenciesCommand { get; set; }

        public ExchangeViewModel()
        {
            this.Title = "Exchange";
            this.Currencies = new List<CompleteCurrency>();
            this.LoadCurrenciesCommand = new Command(async () => await this.ExecuteLoadCurrenciesCommand());
            this.ExchangeCurrenciesCommand = new Command(async () => await this.ExecuteExchangeCurrenciesCommand());
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
                var symbols = await fixer.GetAllCurrencySymbols();
                var rates = await fixer.GetLatestCurrencyExchange();
                if (symbols.IsSuccessful() && rates.IsSuccessful())
                {
                    var fsymbols = (Symbols)symbols;
                    var frates = (Currency)rates;
                    foreach (var symbol in fsymbols.SymbolDictionary)
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

        async Task ExecuteExchangeCurrenciesCommand()
        {
            if (this.IsBusy)
                return;
            this.IsBusy = true;

            var res = double.TryParse(this.FromText, out double from);
            if (res)
            {
                var fromRate = this.Currencies[this.FromSelected].Rate;
                var toRate = this.Currencies[this.ToSelected].Rate;
                this.ToText = (from / fromRate * toRate).ToString();
            }
            else
                // TODO: Mostrare errore a video.

                this.IsBusy = false;
            await Task.FromResult(true);
        }
    }
}
