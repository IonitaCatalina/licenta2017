﻿using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace TestsCollector
{
    [Activity(Label = "Students", MainLauncher = false, Icon = "@drawable/icon")]
    public class StudentTable : ListActivity
    {
        internal List<Models.User> users;

        public StudentTable()
        {
            this.users = Data<Models.User>.ProcessRequest("api/users/Student", "GET", null).ToList();
        }

        protected override void OnCreate(Bundle bundle)
        {
            ListView.FastScrollEnabled = true;

            base.OnCreate(bundle);

            ListAdapter = new StudentTableAdapter(this, users);

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            PatternTable.Photo.StudentId = users[position].Id;
            Toast.MakeText(this, users[position].Fullname, ToastLength.Short).Show();

            //preprocess should return true in order to continue, else, take another pic
            Data<Models.Photo>.ProcessRequest("api/photos", "POST", PatternTable.Photo);

            StartActivity(typeof(PatternTable));
        }
    }
}