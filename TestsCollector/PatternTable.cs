using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace TestsCollector
{
    [Activity(Label = "Patterns", MainLauncher = true, Icon = "@drawable/icon")]
    class PatternTable : ListActivity
    {
        string[] items;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var patterns = Data<Models.Pattern>.GetResult("api/patterns").ToArray();

            items = new string[patterns.Length];

            for (var i = 0; i < patterns.Length; i++)
            {
                items[i] = patterns[i].Name;  
            }

            ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var t = items[position];
            Toast.MakeText(this, t, ToastLength.Short).Show();
        }
    }
}