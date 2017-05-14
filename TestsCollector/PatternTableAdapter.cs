using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace TestsCollector
{
    public class PatternTableAdapter : BaseAdapter<Models.Pattern>
    {
        Activity context;
        List<Models.Pattern> patterns;

        public PatternTableAdapter(Activity context, List<Models.Pattern> objects) : base() {
            this.context = context;
            this.patterns = objects;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Models.Pattern this[int position]
        {
            get { return patterns[position]; }
        }

        public override int Count
        {
            get { return patterns.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = patterns[position].Name;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = patterns[position].Description;
            

            return view;
        }
    }
}