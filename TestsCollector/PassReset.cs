
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace TestsCollector
{
    [Activity(Label = "Password Reset")]
    public class PassReset : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PassReset);

            var resetButton = FindViewById<Button>(Resource.Id.passResetButton);
            var email = FindViewById<EditText>(Resource.Id.resetPassword);

            var incorrectEmailText = FindViewById<TextView>(Resource.Id.incorrectEmailText);
            incorrectEmailText.SetTextColor(Color.Red);

            email.Visibility = Android.Views.ViewStates.Visible;

            resetButton.Visibility = Android.Views.ViewStates.Visible;
            resetButton.Text = "Reset Password";

            incorrectEmailText.Visibility = Android.Views.ViewStates.Gone;

            email.TextChanged += delegate {
                incorrectEmailText.Visibility = Android.Views.ViewStates.Gone;
                incorrectEmailText.Text = "E-mail is not valid!";
            };


            resetButton.Click += delegate {

                if (email.Text == string.Empty)
                {
                    incorrectEmailText.Text = "Please fill in this field with your e-mail!";
                    incorrectEmailText.Visibility = Android.Views.ViewStates.Visible;
                    email.TextAlignment = Android.Views.TextAlignment.Center;
                    return;
                }

                if (email.Text != string.Empty)
                {
                    // check if email is actually valid

                    email.Visibility = Android.Views.ViewStates.Gone;

                    // show text with success info
                    incorrectEmailText.Text = "An e-mail has been sent with the requested information.";
                    incorrectEmailText.Visibility = Android.Views.ViewStates.Visible;

                    resetButton.Text = "Return to the main page";
                    resetButton.Click += delegate
                    {
                        StartActivity(typeof(Login));
                    };

                    }
            };


        }
    }
}