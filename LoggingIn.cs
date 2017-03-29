using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using Android.Transitions;
using System.Reflection.Emit;
using Android.Graphics;

namespace NetworkDetection
{

    [Activity(Label = "LoggingIn")]
    public class LoggingIn : Activity
    {
        static readonly string TAG = typeof(LoggingIn).FullName;

        Button _backButton;
        EditText _serialText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            _backButton = FindViewById<Button>(Resource.Id.letslogin);
            _serialText = FindViewById<EditText>(Resource.Id.serialtext);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _backButton.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Activity1));
                //nextActivity.PutExtra("name",editName.Text);
                StartActivity(nextActivity);
            };
        }
    }
}