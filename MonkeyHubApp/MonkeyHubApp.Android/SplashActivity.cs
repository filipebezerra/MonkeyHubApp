using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace MonkeyHubApp.Droid
{
    [Activity(
        Label = "Monkey Hub",
        Icon = "@drawable/ic_launcher",
        Theme = "@style/SplashTheme",
        MainLauncher = true,
        NoHistory = true
        )]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}