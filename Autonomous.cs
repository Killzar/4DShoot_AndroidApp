using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;

namespace NetworkDetection
{

    [Activity(Label = "Autonomous")]
    public class Autonomous : Activity
    {
        static readonly string TAG = typeof(Autonomous).FullName;

        Button _backButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Auto);

            _backButton = FindViewById<Button>(Resource.Id.nextbutton2);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _backButton.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Controls));
                //nextActivity.PutExtra("name",editName.Text);
                StartActivity(nextActivity);
            };
        }
    }
}