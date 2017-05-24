
using Android.App;
using Android.OS;

namespace TestsCollector
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);
            // Create your application here
        }
    }
}