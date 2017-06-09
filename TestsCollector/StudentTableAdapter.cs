using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace TestsCollector
{
    public class StudentTableAdapter : BaseAdapter<Models.User>
    {
        Activity context;
        List<Models.User> users;

        public StudentTableAdapter(Activity context, List<Models.User> objects) : base() {
            this.context = context;
            this.users = objects;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Models.User this[int position]
        {
            get { return users[position]; }
        }

        public override int Count
        {
            get { return users.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = users[position].Fullname;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = users[position].Email;


            return view;
        }
    }
}