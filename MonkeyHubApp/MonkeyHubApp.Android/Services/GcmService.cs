using System;
using Android.App;
using Android.Content;
using Gcm.Client;
using Android;
using Android.Util;
using System.Text;
using Android.Support.V4.App;
using Android.Media;
using WindowsAzure.Messaging;

[assembly: Permission(Name = MonkeyHubApp.Droid.Helpers.Constants.PERMISSION_C2D_MESSAGE)]
[assembly: UsesPermission(Name = MonkeyHubApp.Droid.Helpers.Constants.PERMISSION_C2D_MESSAGE)]
[assembly: UsesPermission(Name = MonkeyHubApp.Droid.Helpers.Constants.PERMISSION_C2DM_RECEIVE)]
[assembly: UsesPermission(Name = Manifest.Permission.Internet)]
[assembly: UsesPermission(Name = Manifest.Permission.WakeLock)]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = Manifest.Permission.GetAccounts)]
namespace MonkeyHubApp.Droid.Services
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Intent.ActionBootCompleted })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { Helpers.Constants.PACKAGE_ID })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { Helpers.Constants.PACKAGE_ID })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { Helpers.Constants.PACKAGE_ID })]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { Helpers.Constants.FCM_SENDER_ID };
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        private const string TAG = "GcmService";

        private static NotificationHub Hub { get; set; }

        public static void Initialize(Context context)
        {
            try
            {
                var cs = ConnectionString.CreateUsingSharedAccessKeyWithListenAccess(
                    new Java.Net.URI(MonkeyHubApp.Helpers.Constants.AzureServiceBusUrl),
                    MonkeyHubApp.Helpers.Constants.AzureHubSecret);
                Hub = new NotificationHub(MonkeyHubApp.Helpers.Constants.AzureHubName, cs, context);
            }
            catch(Exception ex)
            {
                Log.Error(TAG, $"[Initialize] Unable to initialize Notification Hub. Error = {ex.Message}"); 
            }
        }

        public static void Register(Context context)
        {
            try
            {
                GcmClient.Register(context, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch(Exception ex)
            {
                Log.Error(TAG, $"[Register] Unable to register GCM. Error = {ex.Message}");
            }
        }

        public GcmService() : base(PushHandlerBroadcastReceiver.SENDER_IDS) {}

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(TAG, $"GCM registered = {registrationId}");

            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch(Exception ex)
            {
                Log.Error(TAG, $"[OnRegistered] Unable to unregister Notification Hub. Error = {ex.Message}");
            }

            try
            {
                Hub.Register(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"[OnRegistered] Unable to register Notification Hub. Error = {ex.Message}");
            }            
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(TAG, $"GCM unregistered = {registrationId}");

            try
            {
                Hub.Unregister();
            }
            catch(Exception ex)
            {
                Log.Error(TAG, $"[OnUnRegistered] Unable to unregister Notification Hub. Error = {ex.Message}");
            }
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Error(TAG, $"[OnRecoverableError] GCM recoverable error = {errorId}");
            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(TAG, $"GCM error = {errorId}");
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Error(TAG, "[OnMessage] GCM message received");

            var message = intent.Extras.GetString("message");
            if (!string.IsNullOrWhiteSpace(message))
            {
                CreateNotification("Monkey Hub", message);
            }
            else
            {
                var entries = new StringBuilder();
                foreach(var key in intent.Extras.KeySet())
                {
                    entries.AppendLine($"{key}={intent.Extras.Get(key).ToString()}");
                }
                Log.Verbose(TAG, $"GCM message received with no message param. Intent = {entries.ToString()}");
            }
        } 

        private void CreateNotification(string title, string content)
        {
            var notificationManager = NotificationManagerCompat.From(this);
            var notificationIntent = new Intent(this, typeof(MainActivity))
                .AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

            var notificationStyle = new NotificationCompat.BigTextStyle()
                .BigText(content);

            var notification = new NotificationCompat.Builder(this)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetAutoCancel(true)
                .SetStyle(notificationStyle)
                .SetTicker(title)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .Build();

            notification.Defaults = NotificationDefaults.All;
            notificationManager.Notify(Helpers.Settings.GetUniqueNotificationId(), notification);
        }
    }
}