using Android.App;
using Android.Widget;
using Android.OS;

namespace TestsCollector
{
    [Activity(Label = "Tests Collector", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var loginButton = FindViewById<Button>(Resource.Id.loginButton);
            var registerButton = FindViewById<Button>(Resource.Id.registerButton);

            loginButton.Click += delegate
            {
                StartActivity(typeof(Login));
            };

            registerButton.Click += delegate
            {
                StartActivity(typeof(Register));
            };
        }
    }
}

