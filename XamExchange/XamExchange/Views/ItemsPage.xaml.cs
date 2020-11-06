namespace XamExchange.Views
{
    using System;
    using System.ComponentModel;
    using Xamarin.Forms;

    using XamExchange.Models;
    using XamExchange.ViewModels;

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        readonly CurrenciesViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CurrenciesViewModel();
        }

        async void OnCurrencySelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is CompleteCurrency item))
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            CurrencyListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Currencies.Count == 0)
                viewModel.LoadCurrenciesCommand.Execute(null);
        }
    }
}