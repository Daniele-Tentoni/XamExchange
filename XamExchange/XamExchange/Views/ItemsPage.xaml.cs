﻿namespace XamExchange.Views
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
            this.InitializeComponent();

            this.BindingContext = this.viewModel = new CurrenciesViewModel();
        }

        async void OnCurrencySelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is CompleteCurrency item))
                return;

            await this.Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            this.CurrencyListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e) => await this.Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.viewModel.Currencies.Count == 0)
                this.viewModel.LoadCurrenciesCommand.Execute(null);
        }
    }
}