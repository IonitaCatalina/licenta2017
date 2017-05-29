
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using TestsCollector.Helpers;

namespace TestsCollector
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            // buttons
            var loginButton = FindViewById<Button>(Resource.Id.loginPageButton);
            var fbLoginButton = FindViewById<ImageButton>(Resource.Id.facebookButton);
            var googleLoginButton = FindViewById<ImageButton>(Resource.Id.GoogleButton);

            // inputs
            var email = FindViewById<EditText>(Resource.Id.email);
            var password = FindViewById<EditText>(Resource.Id.password);

            // text views
            var incorrectEmailAlert = FindViewById<TextView>(Resource.Id.incorrectEmail);
            var incorrectPassAlert = FindViewById<TextView>(Resource.Id.IncorrectPassword);
            var passReset = FindViewById<TextView>(Resource.Id.passReset);

            incorrectEmailAlert.SetTextColor(Color.Red);
            incorrectPassAlert.SetTextColor(Color.Red);

            incorrectEmailAlert.Visibility = Android.Views.ViewStates.Gone;
            incorrectPassAlert.Visibility = Android.Views.ViewStates.Gone;

            email.TextChanged += delegate {
                incorrectEmailAlert.Visibility = Android.Views.ViewStates.Gone;
                incorrectEmailAlert.Text = "E-mail is not valid!";
            };

            password.TextChanged += delegate
            {
                incorrectPassAlert.Visibility = Android.Views.ViewStates.Gone;
                incorrectPassAlert.Text = "Incorrect password!";
            };

            loginButton.Click += delegate {

                if (email.Text == string.Empty)
                {
                    incorrectEmailAlert.Text = "Please fill in this field with your e-mail!";
                    incorrectEmailAlert.Visibility = Android.Views.ViewStates.Visible;
                    return;
                }


                if (password.Text == string.Empty)
                {
                    incorrectPassAlert.Text = "Please fill in this field with your password!";
                    incorrectPassAlert.Visibility = Android.Views.ViewStates.Visible;
                    return;
                }

                var user = new Models.User
                {
                    Email = email.Text,
                    Password = password.Text,
                };

                if (email.Text != string.Empty)
                {
                    if (password.Text != string.Empty)
                    {
                        if (user.CheckCredentials() == "BadRequest")
                        {
                            incorrectPassAlert.Text = "Incorrect e-mail or password!";
                            incorrectPassAlert.Visibility = Android.Views.ViewStates.Visible;
                            return;
                        }

                        if (user.CheckCredentials() == string.Empty)
                        {
                            incorrectPassAlert.Text = "User does not exist!";
                            incorrectPassAlert.Visibility = Android.Views.ViewStates.Visible;
                            return;
                        }

                        StartActivity(typeof(PatternTable));
                    }
                }
            };

            fbLoginButton.Click += delegate {
                //fb call
                StartActivity(typeof(PatternTable));
            };

            googleLoginButton.Click += delegate {
                //google call
                StartActivity(typeof(PatternTable));
            };

            passReset.Click += delegate
            {
                StartActivity(typeof(PassReset));
            };

        }
    }
}