using MonkeyHubApp.Notification;
using Gcm.Client;
using Xamarin.Forms;
using MonkeyHubApp.Droid.Services;

[assembly: Dependency(typeof(MonkeyHubApp.Droid.Providers.PushNotification))]
namespace MonkeyHubApp.Droid.Providers
{
    public class PushNotification : IPushNotification
    {
        public void RegisterForPushNotification()
        {
            GcmClient.CheckDevice(Forms.Context);
            GcmClient.CheckManifest(Forms.Context);
            GcmService.Initialize(Forms.Context);
            GcmService.Register(Forms.Context);
        }
    }
}