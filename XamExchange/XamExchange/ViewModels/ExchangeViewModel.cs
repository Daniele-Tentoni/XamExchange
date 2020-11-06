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
                OnPropertyChanged(nameof(LastUpdate));
            }
        }

        public ObservableCollection<CompleteCurrency> Currencies { get; set; }

        public ObservableCollection<string> PickerCurrencies { get; set; }

        private int _fromSelected;
        public int FromSelected
        {
            get => _fromSelected;
            set => SetProperty(ref _fromSelected, value);
        }
        private int _toSelected;
        public int ToSelected
        {
            get => _toSelected;
            set => SetProperty(ref _toSelected, value);
        }

        private string _fromText;
        public string FromText
        {
            get => _fromText;
            set => SetProperty(ref _fromText, value);
        }
        private string _toText;
        public string ToText
        {
            get => _toText;
            set => SetProperty(ref _toText, value);
        }

        public Command LoadCurrenciesCommand { get; set; }

        public Command ExchangeCurrenciesCommand { get; set; }

        public Command RefreshCurrenciesCommand { get; set; }

        public ExchangeViewModel()
        {
            Title = "Exchange";
            Currencies = new ObservableCollection<CompleteCurrency>();
            PickerCurrencies = new ObservableCollection<string>();
            Currencies.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (CompleteCurrency item in e.NewItems)
                    {
                        PickerCurrencies.Add($"{item.Code}");
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    PickerCurrencies.Clear();
                }
            };
            LoadCurrenciesCommand = new Command(async () => await ExecuteLoadCurrenciesCommand());
            ExchangeCurrenciesCommand = new Command(async () => await ExecuteExchangeCurrenciesCommand());
            RefreshCurrenciesCommand = new Command(async () => await ExecuteRefreshCurrenciesCommand());
        }

        private async Task ExecuteLoadCurrenciesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Currencies.Clear();
                var dataStore = DependencyService.Get<CurrencyDataStore>();
                var currencies = await dataStore.GetItemsAsync();
                foreach(var currency in currencies)
                {
                    Currencies.Add(currency);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteExchangeCurrenciesCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var res = double.TryParse(FromText, out double from);
                if (res)
                {
                    var fromRate = Currencies[FromSelected].Rate;
                    var toRate = Currencies[ToSelected].Rate;
                    ToText = (from / fromRate * toRate).ToString();
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
                IsBusy = false;
            }
        }

        async Task ExecuteRefreshCurrenciesCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await App.LoadFixerData();
                IsBusy = false;
                await ExecuteLoadCurrenciesCommand();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}