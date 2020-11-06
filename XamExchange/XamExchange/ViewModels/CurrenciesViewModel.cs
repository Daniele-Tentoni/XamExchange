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
            Title = "Currencies";
            Currencies = new ObservableCollection<CompleteCurrency>();
            LoadCurrenciesCommand = new Command(async () => await ExecuteLoadCurrenciesCommand());

            MessagingCenter.Subscribe<NewItemPage, CompleteCurrency>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item;
                Currencies.Add(newItem);
                await DataStore.AddOrUpdateItemAsync(newItem);
            });
        }

        async Task ExecuteLoadCurrenciesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Currencies.Clear();

                var dataStore = DependencyService.Get<CurrencyDataStore>();
                var currencies = await dataStore.GetItemsAsync();
                foreach (var currency in currencies)
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
    }
}