namespace XamExchange
{
    using Xamarin.Forms;
    using XamExchange.Services;
    using XamExchange.Views;

    public partial class App : Application
    {

        public App()
        {
            this.InitializeComponent();

            DependencyService.Register<CurrencyDataStore>();
            this.MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
