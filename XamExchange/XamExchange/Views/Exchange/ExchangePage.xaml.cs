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
            InitializeComponent();
            BindingContext = viewModel = new ExchangeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Currencies.Count == 0)
                viewModel.LoadCurrenciesCommand.Execute(null);
        }

        void Entry_TextChanged(object sender, TextChangedEventArgs e) =>
            // TODO: Put this binding in the xaml.
            viewModel.ExchangeCurrenciesCommand.Execute(null);
    }
}