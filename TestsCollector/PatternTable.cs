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
    [Activity(Label = "Patterns", MainLauncher = true, Icon = "@drawable/icon")]
    public class PatternTable : ListActivity
    {
        internal List<Models.Pattern> patterns;
        int CAMERA_PIC_REQUEST = 1337;

        public static class App
        {
            public static File _file;
            public static File _dir;
            public static Bitmap bitmap;
        }

        public PatternTable()
        {
            this.patterns = Data<Models.Pattern>.ProcessRequest("api/patterns", "GET", null).ToList();
        }

        public List<Models.Pattern> Patterns { get => patterns; set => patterns = value; }

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

            using (var stream = new System.IO.MemoryStream())
            {
                file.Compress(Bitmap.CompressFormat.Png, 0, stream);
                var bitmapData = stream.ToArray();

                var pattern = new Models.Photo
                {
                    Name = App._file.Name,
                    Image = bitmapData
                };

                Data<Models.Photo>.ProcessRequest("api/photos", "POST", pattern);
            }

            GC.Collect();
        }
    }
}
