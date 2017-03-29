using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using Android.Transitions;
using System.Reflection.Emit;
using Android.Graphics;

namespace NetworkDetection
{

    [Activity(Label = "Mapping")]
    public class Mapping :Activity
    {
        static readonly string TAG = typeof(Mapping).FullName;

        Button _backButton;
        Label label, label2;
        Slide sliderImage;
        Shader sliderColor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Maps);

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