namespace XamExchange.Views
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    using XamExchange.Models;
    using XamExchange.Views.About;
    using XamExchange.Views.Exchange;

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        readonly Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            this.InitializeComponent();

            this.MasterBehavior = MasterBehavior.Popover;

            this.MenuPages.Add((int)MenuItemType.Exchange, (NavigationPage)this.Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!this.MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Exchange:
                        this.MenuPages.Add(id, new NavigationPage(new ExchangePage()));
                        break;
                    case (int)MenuItemType.Currencies:
                        this.MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        this.MenuPages.Add(id, new NavigationPage(new AboutTabPage()));
                        break;
                }
            }

            var newPage = this.MenuPages[id];

            if (newPage != null && this.Detail != newPage)
            {
                this.Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                this.IsPresented = false;
            }
        }
    }
}