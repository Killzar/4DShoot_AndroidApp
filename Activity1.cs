using Android.App;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Widget;
using Android.Util;
using System.Net.Sockets;
using System.Text;
using Android.Content;

namespace NetworkDetection
{

	[Activity(Label = "4DControls", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
		static readonly string TAG = typeof(Activity1).FullName;

        Socket socket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

        private ImageView _isConnectedImage;
        private ImageView _wifiImage;
		TextView _connectionType;
        TextView _ipText;
        Button _ledButton;
        Button _ledOffButton;
        Button _controlsButton;
        Button _sendCommandButton;
        Button _buttonLogin;
        TextView _errorsText;
        EditText _editText;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _wifiImage = FindViewById<ImageView>(Resource.Id.wifi_image);
            _buttonLogin = FindViewById<Button>(Resource.Id.buttonLogin);
            _isConnectedImage = FindViewById<ImageView>(Resource.Id.is_connected_image);
			_connectionType = FindViewById<TextView>(Resource.Id.connection_type_text);
            _ipText = FindViewById<TextView>(Resource.Id.ip_text);
            _ledButton = FindViewById<Button>(Resource.Id.button1);
            _ledOffButton = FindViewById<Button>(Resource.Id.button2);
            _editText = FindViewById<EditText>(Resource.Id.editText);
            _errorsText = FindViewById<TextView>(Resource.Id.errorstext);
            _sendCommandButton = FindViewById<Button>(Resource.Id.sendcommandbutton);
            _controlsButton = FindViewById<Button>(Resource.Id.nextbutton);
        }

		protected override void OnResume()
		{
			base.OnResume();
			DetectNetwork();
            Connected();

            //Sending Buttons Over Wifi
            byte[] myByteArray = Encoding.ASCII.GetBytes("Default");
            _ledButton.Click += delegate {
                try
                {
                    myByteArray = Encoding.ASCII.GetBytes("LED ON");
                    socket.Send(myByteArray, myByteArray.Length, SocketFlags.None);
                }
                catch
                {
                    _errorsText.Text = "Cannot Send LED ON";
                }
            };
            _ledOffButton.Click += delegate {
                try
                {
                    myByteArray = Encoding.ASCII.GetBytes("LED OFF");
                    socket.Send(myByteArray, myByteArray.Length, SocketFlags.None);
                }
                catch
                {
                    _errorsText.Text = "Cannot Send LED OFF";
                }
            };
            _sendCommandButton.Click += (s, e) =>
            {
                try
                {
                    myByteArray = Encoding.ASCII.GetBytes(_editText.Text.ToString());
                    socket.Send(myByteArray, myByteArray.Length, SocketFlags.None);
                }
                catch
                {
                    _errorsText.Text = "Cannot Send " + _editText.Text.ToString();
                }
            };

            //Navigation
            _buttonLogin.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(LoggingIn));
                StartActivity(nextActivity);
            };
            _controlsButton.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(Controls));
                StartActivity(nextActivity);
            };
        }

        private void DetectNetwork()
        {
			ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo info = connectivityManager.ActiveNetworkInfo;
			bool isOnline = info.IsConnected;

			Log.Debug(TAG, "IsOnline = {0}", isOnline);

			if (isOnline)
			{
				_isConnectedImage.SetImageResource(Resource.Drawable.green_square);

				// Display the type of connectionn
				NetworkInfo.State activeState = info.GetState();
				_connectionType.Text = info.TypeName;
                _ipText.Text = GetIP();


                // Check for a WiFi connection
                bool isWifi = info.Type == ConnectivityType.Wifi;
				if(isWifi)
				{
					Log.Debug(TAG, "Wifi connected.");
					_wifiImage.SetImageResource(Resource.Drawable.green_square);
				} else
				{
					Log.Debug(TAG, "Wifi disconnected.");
					_wifiImage.SetImageResource(Resource.Drawable.red_square);
				}
			} else
			{
				_isConnectedImage.SetImageResource(Resource.Drawable.red_square);
				_wifiImage.SetImageResource(Resource.Drawable.red_square);
				_connectionType.Text = "N/A";
                _ipText.Text = "N/A";
			}
        }

        public string GetIP()
        {
            WifiManager wifimanager = (WifiManager)GetSystemService(WifiService);
            WifiInfo wifiinfo = wifimanager.ConnectionInfo;
            int ip = wifiinfo.IpAddress;

            string ipString = string.Format("{0}.{1}.{2}.{3}", (ip & 0xff), (ip >> 8 & 0xff), (ip >> 16 & 0xff), (ip >> 24 & 0xff));

            return ipString;
        }
        public int GetPort()
        {
            return 80;
        }
        public void Connected()
        {
            try
            {
                socket.Connect("192.168.4.1", 80);
                _errorsText.Text = "Connected";
            }
            catch(System.Net.Sockets.SocketException)
            {
                _errorsText.Text = "Cannot Connect";
            }
        }

    }
}
