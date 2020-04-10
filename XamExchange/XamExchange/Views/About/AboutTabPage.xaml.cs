namespace XamExchange.Views.About
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using XamExchange.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutTabPage : TabbedPage
    {
        public AboutTabPage()
        {
            this.InitializeComponent();
            this.BindingContext = new AboutViewModel();
        }
    }
}