using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using System.Net.Sockets;
using System.Text;

namespace NetworkDetection
{

    [Activity(Label = "Controls")]
    public class Controls : Activity1
    {
        static readonly string TAG = typeof(Controls).FullName;

        Button _controlsButton;
        Button _mapsButton;
        Button _autoButton;
        SeekBar _seekbarLeft;
        SeekBar _seekbarRight;

        Socket socket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Controller);

            _controlsButton = FindViewById<Button>(Resource.Id.nextbutton2);
            _autoButton = FindViewById<Button>(Resource.Id.nextauto);
            _mapsButton = FindViewById<Button>(Resource.Id.nextmap);
            _seekbarLeft = FindViewById<SeekBar>(Resource.Id.seekleft);
            _seekbarRight = FindViewById<SeekBar>(Resource.Id.seekright);
           // _seekbarRight.SetOnSeekBarChangeListener(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Connected();

            byte[] myByteArray = Encoding.ASCII.GetBytes("Default");
            _controlsButton.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Activity1));
                //nextActivity.PutExtra("name",editName.Text);
                StartActivity(nextActivity);
            };
            _autoButton.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Autonomous));
                //nextActivity.PutExtra("name",editName.Text);
                StartActivity(nextActivity);
            };
            _mapsButton.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Mapping));
                //nextActivity.PutExtra("name",editName.Text);
                StartActivity(nextActivity);
            };
            _seekbarLeft.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                if (e.Progress >= 50)
                {
                    myByteArray = Encoding.ASCII.GetBytes("LFT_FWD " + (e.Progress-50)*2.56);
                    socket.Send(myByteArray, myByteArray.Length, SocketFlags.None);
                }
                else
                {
                    myByteArray = Encoding.ASCII.GetBytes("LFT_BCK " + (e.Progress - 50) * 2.56);
                    socket.Send(myByteArray, myByteArray.Length, SocketFlags.None);
                }
            };
            _seekbarRight.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                if(e.Progress >= 50)
                {
                    myByteArray = Encoding.ASCII.GetBytes("RGT_FWD " + (e.Progress - 50) * 2.56);
                    socket.Send(myByteArray, myByteArray.Length, SocketFlags.None);
                }
                else
                {
                    myByteArray = Encoding.ASCII.GetBytes("RGT_BCK " + (e.Progress - 50) * 2.56);
                    socket.Send(myByteArray, myByteArray.Length, SocketFlags.None);
                }
            };

        }
        public void Connected()
        {
            try
            {
                socket.Connect("192.168.4.1", 80);
            }
            catch (System.Net.Sockets.SocketException)
            {

            }
        }
    }
}