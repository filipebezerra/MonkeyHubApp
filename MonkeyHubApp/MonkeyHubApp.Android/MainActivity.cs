﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MonkeyHubApp.Droid
{
    [Activity(
        Label = "Monkey Hub", 
        Icon = "@drawable/ic_launcher",
        LaunchMode = LaunchMode.SingleTask,
        Theme = "@style/MainTheme", 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            LoadApplication(new App());
        }
    }
}

