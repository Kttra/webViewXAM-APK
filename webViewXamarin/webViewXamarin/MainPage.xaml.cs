using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using webViewXamarin;

namespace webViewXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            webView.Source = "https://www.bing.com";
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            webView.Source = "https://www.yahoo.com";

            /*
                if(webView.CanGoForward)
                    webView.GoForward();
                else if(webView.CanGoBack)
                    webView.GoBack();
            */
        }
        //User Agent strings
        public const string mobileUA = "Mozilla/5.0 (iPhone; CPU iPhone OS 13_5_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1.1 Mobile/15E148 Safari/604.1";
        public const string edgeUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.59";
        public const string desktopUA = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
        public const string firefoxUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:98.0) Gecko/20100101 Firefox/98.0";
    }
}

