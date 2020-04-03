namespace XamExchange.Views.Exchange
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using XamExchange.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangePage : ContentPage
    {
        readonly ExchangeViewModel viewModel;

        public ExchangePage()
        {
            this.InitializeComponent();
            this.BindingContext = this.viewModel = new ExchangeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.viewModel.Currencies.Count == 0)
                this.viewModel.LoadCurrenciesCommand.Execute(null);
        }
    }
}