namespace XamExchange.ViewModels
{
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async (arg) => await Browser.OpenAsync((string)arg));
        }

        public ICommand OpenWebCommand { get; }
    }
}