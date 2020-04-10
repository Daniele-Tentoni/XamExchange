using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamExchange.Views.About
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutTabPage : TabbedPage
    {
        public AboutTabPage()
        {
            InitializeComponent();
        }
    }
}