/*  Mobile Android App used to load random webpages for testing.
 *  The program will run through edge, firefox, and mobile webpages.
*/

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Android.Webkit;
using System;
using System.Linq;
//using System.IO;
using System.Threading; //Async
using System.Threading.Tasks; //Tasks

namespace MultiTabbedApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        WebView webView;
        TextView ProgressText;
        CancellationTokenSource cts = new CancellationTokenSource();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
            //Setup webview
            webView = FindViewById<WebView>(Resource.Id.webview);
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.DomStorageEnabled = true;

            //Prevent a different browser like chrome from opening
            webView.SetWebViewClient(new WebViewClient());

            //Allows for loading the webpage
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            webView.LoadUrl("https://play.google.com/store/apps");
            webView.Settings.LoadWithOverviewMode = true;

            //Setup progress text
            ProgressText = (TextView)FindViewById(Resource.Id.text_view_id);
            ProgressText.Text = "Welcome";

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        //The bottom navigation bar, you can change the text in resources -> menu -> navigation.xml
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                //Run button, 
                case Resource.Id.navigation_home:
                    RunClicked();
                    return true;
                //Mobile button, loads your set webpage, stops everything
                case Resource.Id.navigation_dashboard:
                    MobileRun();
                    return true;
                case Resource.Id.navigation_notifications:
                    StopAll();
                    return true;
            }
            return false;
        }
        //When the Run button is pressed
        private async void RunClicked()
        {
            //Check if the task is currently running
            if (Globals.inProgress == false)
            {
                //Reset everything
                Globals.inProgress = true;

                //Create new token
                cts = new CancellationTokenSource();

                //Run the tasks edge -> desktop -> mobile
                await Edge(cts.Token);
                cts.Dispose();
            }
            //If the task is running, cancel everything
            else
            {
                cts.Cancel();
                Globals.inProgress = false;
                cts.Dispose();
                ProgressText.Text = "Attempting to stop...";
            }
        }
        //Mobile button, loads your set webpage, stops everything different method than cts
        private void MobileRun()
        {
            if (Globals.inProgress)
            {
                ProgressText.Text = "Attempting to run mobile...";
                Globals.request_End = true;
                webView.Settings.UserAgentString = mobileUA;
                webView.LoadUrl("https://www.google.com");
            }
            else
            {
                webView.Settings.UserAgentString = mobileUA;
                webView.LoadUrl("https://www.google.com");
                ProgressText.Text = "Task Completed";
            }
        }
        //Stop all tasks
        private void StopAll()
        {
            //Cancel all the async methods
            try
            {
                ProgressText.Text = "Attempting to stop...";
                cts.Cancel();
                CheckTime();
            }
            catch
            {
                ProgressText.Text = "Nothing to Stop.";
            }
            finally
            {
                webView.Settings.UserAgentString = firefoxUA;
                webView.LoadUrl("https://www.google.com");
                cts.Dispose();
            }
        }
        //cts.cancel timed out, there is nothing to stop
        private async void CheckTime()
        {
            await Task.Delay(8000);
            if (ProgressText.Text == "Attempting to stop...")
            {
                ProgressText.Text = "Nothing to stop.";
            }
        }
        //Edge Task
        private async Task Edge(CancellationToken cancellationToken)
        {
            //Change User Agent to edge
            webView.Settings.UserAgentString = edgeUA;
            for (int i = 0; i < Globals.edgeMaxTask; i++)
            {
                //Two ways to end the async method
                if (cancellationToken.IsCancellationRequested)
                {
                    ProgressText.Text = "Stopped!";
                    Globals.inProgress = false;
                    return;
                }
                if (Globals.request_End)
                {
                    Globals.request_End = false;
                    Globals.inProgress = false;
                    ProgressText.Text = "Success!";
                    return;
                }

                //Load random webpage
                string address = "https://www.google.com/search?q=define+" + Globals.RandomLetters(Globals.RandomNumber(7));
                webView.LoadUrl(address);

                //Update Progress
                ProgressText.Text = "Edge Progress " + (i+1) + "/" + Globals.edgeMaxTask;

                //Wait
                await Task.Delay(Globals.RandomTime(7500));
            }

            await Desktop(cts.Token);
            await Mobile(cts.Token);
        }
        //Desktop Task
        private async Task Desktop(CancellationToken cancellationToken)
        {
            //Change User Agent to chrome/desktop
            webView.Settings.UserAgentString = firefoxUA;

            for (int i = 0; i < Globals.desktopMaxTask; i++)
            {
                //Two ways to end the async method
                if (cancellationToken.IsCancellationRequested)
                {
                    ProgressText.Text = "Stopped!";
                    Globals.inProgress = false;
                    return;
                }
                if (Globals.request_End)
                {
                    Globals.request_End = false;
                    Globals.inProgress = false;
                    ProgressText.Text = "Success!";
                    return;
                }

                //Load random webpage
                string address = "https://www.google.com/search?q=define+" + Globals.RandomLetters(Globals.RandomNumber(7));
                webView.LoadUrl(address);

                //Update Progress
                ProgressText.Text = "Desktop Progress " + (i + 1) + "/" + Globals.desktopMaxTask;

                //Wait
                await Task.Delay(Globals.RandomTime(7500));
            }
        }
        //Mobile Task
        private async Task Mobile(CancellationToken cancellationToken)
        {
            await Task.Delay(Globals.RandomTime(7500));

            //Set UserAgent to mobile
            webView.Settings.UserAgentString = mobileUA;

            for (int i = 0; i < Globals.mobileMaxTask; i++)
            {
                //Two ways to end the async method
                if (cancellationToken.IsCancellationRequested)
                {
                    ProgressText.Text = "Stopped!";
                    Globals.inProgress = false;
                    return;
                }
                if (Globals.request_End)
                {
                    Globals.request_End = false;
                    Globals.inProgress = false;
                    ProgressText.Text = "Success!";
                    return;
                }

                //Load random webpage
                string address = "https://www.google.com/search?q=define+" + Globals.RandomLetters(Globals.RandomNumber(7));
                webView.LoadUrl(address);

                //Update Progress
                ProgressText.Text = "Mobile Progress " + (i + 1) + "/" + Globals.mobileMaxTask;

                //Wait
                await Task.Delay(Globals.RandomTime(7500));

                //All asyncs are done
                ProgressText.Text = "Done!";
                Globals.inProgress = false;
            }
        }

        //User Agent strings
        private const string mobileUA = "Mozilla/5.0 (iPhone; CPU iPhone OS 13_5_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1.1 Mobile/15E148 Safari/604.1";
        private const string edgeUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.59";
        //private const string desktopUA = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
        private const string firefoxUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:98.0) Gecko/20100101 Firefox/98.0";
    }
    static class Globals
    {
        //Number of searches to do for each category
        public static int edgeMaxTask = 6;
        public static int mobileMaxTask = 22;
        public static int desktopMaxTask = 32;
        //Whether or not the task is running
        public static bool inProgress = false;
        //Cancel requests for specific tasks, alternative to cts
        public static bool request_End = false;

        //Global functions
        public static Random random = new Random();
        //Generate random string with only letters
        public static string RandomLetters(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //Generate random integer with addition and power function
        public static int RandomTime(int length)
        {
            return random.Next(5000, length);
        }
        public static int RandomNumber(int length)
        {
            return random.Next(3, length);
        }
    }
}

