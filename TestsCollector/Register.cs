
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace TestsCollector
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            var firstName = FindViewById<EditText>(Resource.Id.firstName);
            var lastName = FindViewById<EditText>(Resource.Id.lastName);
            var email = FindViewById<EditText>(Resource.Id.emailRegister);
            var password = FindViewById<EditText>(Resource.Id.registerPassword);
            var passwordConfirm = FindViewById<EditText>(Resource.Id.registerPasswordConfirm);

            var errorMessage = FindViewById<TextView>(Resource.Id.passDontMatch);
            errorMessage.SetTextColor(Color.Red);
            errorMessage.Visibility = Android.Views.ViewStates.Gone;

            password.TextChanged += delegate {
                errorMessage.Visibility = Android.Views.ViewStates.Gone;
            };

            passwordConfirm.TextChanged += delegate {
                errorMessage.Visibility = Android.Views.ViewStates.Gone;
            };

            var checkedRadioButton = string.Empty;
            var radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);

            radioGroup.CheckedChange += delegate {
                checkedRadioButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId).Text;
            };



            var registerButton = FindViewById<Button>(Resource.Id.RegisterPageButton);

            registerButton.Click += delegate {
                if (firstName.Text != string.Empty &&
                    lastName.Text != string.Empty &&
                    email.Text != string.Empty &&
                    password.Text != string.Empty &&
                    passwordConfirm.Text != string.Empty &&
                    checkedRadioButton != string.Empty
                )
                {
                    var user = new Models.User {
                        Email = email.Text,
                        Username = string.Format("{0}{1}", firstName.Text.Substring(0, 1), lastName.Text),
                        Password = password.Text,
                        ConfirmPassword = passwordConfirm.Text,
                        FirstName = firstName.Text,
                        LastName = lastName.Text
                    };

                if (password.Text.Equals(passwordConfirm.Text))
                    {
                        Data<Models.User>.ProcessRequest("api/users/create", "POST", user);

                        //check respose? 

                        StartActivity(typeof(Login));
                    }
                    else
                    {
                        errorMessage.Visibility = Android.Views.ViewStates.Visible;
                    }
                }                
            };
        }
    }
}