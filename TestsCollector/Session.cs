using Android.Content;
using Android.Preferences;

namespace TestsCollector
{
    public class Session
    {
        private static ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

         public static string CurrentUserEmail { get; set; }

        public Session(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void saveAccessKey(string key)
        {
            mPrefsEditor.PutString(CurrentUserEmail, key);
            mPrefsEditor.Commit();
        }

        public static string getAccessKey()
        {
            return mSharedPrefs.GetString(CurrentUserEmail, "");
        }
    }
}