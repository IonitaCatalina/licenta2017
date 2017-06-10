using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestsCollector
{
    [Activity(Label = "Patterns", MainLauncher = false, Icon = "@drawable/icon")]
    public class PatternTable : ListActivity
    {
        internal List<Models.Pattern> patterns;
        public static Models.Photo Photo { get; set; }

        public static class App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }

        public PatternTable()
        {
            if(Session.getAccessKey() != string.Empty)
                this.patterns = Data<Models.Pattern>.ProcessRequest($"api/{Session.getAccessKey()}/patterns", "GET", null).ToList();
        }

        protected override void OnCreate(Bundle bundle)
        {
            ListView.FastScrollEnabled = true;

            base.OnCreate(bundle);

            ListAdapter = new PatternTableAdapter(this, patterns);

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var t = patterns[position].Name;
            Toast.MakeText(this, t, ToastLength.Short).Show();

            Photo = new Models.Photo { PatternId = patterns[position].Id };
            CreateDirectoryForPictures();
            TakeAPicture();
        }

        public void TakeAPicture()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, String.Format("test_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                     Android.OS.Environment.DirectoryPictures), $"Tests {DateTime.Now.ToString("yyyy-MM-dd")}");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent intent = new Intent(Intent.ActionGetContent);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);

            Bitmap file = BitmapFactory.DecodeFile(App._file.Path);

            if (file != null)
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    var bitmapScaled = Bitmap.CreateScaledBitmap(file, file.Width/10, file.Height/10, true);
                    bitmapScaled.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    var bitmapData = stream.ToArray();

                    file.Recycle();

                    //add photo properties
                    Photo.Name = App._file.Name;
                    Photo.Image = bitmapData;
                    Photo.TeacherId = Session.getAccessKey();                       
                   
                    StartActivity(typeof(StudentTable));
                }
            }

            GC.Collect();
        }
    }
}
