namespace XamExchange.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    using XamExchange.Models;
    using XamExchange.Services;
    using XamExchange.Views;

    public class CurrenciesViewModel : BaseViewModel
    {
        public ObservableCollection<Rate> Currencies { get; set; }
        public Command LoadCurrenciesCommand { get; set; }

        public CurrenciesViewModel()
        {
            this.Title = "Browse";
            this.Currencies = new ObservableCollection<Rate>();
            this.LoadCurrenciesCommand = new Command(async () => await this.ExecuteLoadCurrenciesCommand());

            MessagingCenter.Subscribe<NewItemPage, Rate>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Rate;
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
                var fixerDataStore = new FixerDataStore();
                var result = await fixerDataStore.GetLatestCurrencyExchange();
                var latest = result;
                if (!latest.IsSuccessful())
                    Debug.WriteLine("Error.");
                else
                {
                    foreach (var item in ((Currency)latest).Rates)
                    {
                        this.Currencies.Add(new Rate { Name = item.Key, Value = (double)item.Value });
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