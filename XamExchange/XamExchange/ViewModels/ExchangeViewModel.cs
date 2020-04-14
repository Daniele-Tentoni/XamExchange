namespace XamExchange.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using XamExchange.Models;
    using XamExchange.Models.FixerModels;
    using XamExchange.Services;

    public class ExchangeViewModel : BaseViewModel
    {
        public DateTime LastUpdate
        {
            get => Preferences.Get("last_update", DateTime.Now);
            set {
                Preferences.Set("last_update", value);
                this.OnPropertyChanged(nameof(this.LastUpdate));
            }
        }

        public ObservableCollection<CompleteCurrency> Currencies { get; set; }

        public ObservableCollection<string> PickerCurrencies { get; set; }

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
            this.Currencies = new ObservableCollection<CompleteCurrency>();
            this.PickerCurrencies = new ObservableCollection<string>();
            this.Currencies.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (CompleteCurrency item in e.NewItems)
                    {
                        this.PickerCurrencies.Add($"{item.Code} {item.Name}");
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    this.PickerCurrencies.Clear();
                }
            };
            this.LoadCurrenciesCommand = new Command(async () => await this.ExecuteLoadCurrenciesCommand());
            this.ExchangeCurrenciesCommand = new Command(async () => await this.ExecuteExchangeCurrenciesCommand());
        }

        private async Task ExecuteLoadCurrenciesCommand()
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
                    this.LastUpdate = DateTimeOffset.FromUnixTimeSeconds(frates.Timestamp).LocalDateTime;
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

        async Task ExecuteExchangeCurrenciesCommand()
        {
            if (this.IsBusy)
                return;
            this.IsBusy = true;

            try
            {
                var res = double.TryParse(this.FromText, out double from);
                if (res)
                {
                    var fromRate = this.Currencies[this.FromSelected].Rate;
                    var toRate = this.Currencies[this.ToSelected].Rate;
                    this.ToText = (from / fromRate * toRate).ToString();
                }
                else
                    Debug.WriteLine("[Exchange] Parse error.");
                await Task.FromResult(true);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
