using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MonkeyHubApp.Droid.Helpers
{
    public static class Settings
    {
        public static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants
        private const string NotificationIdKey = "notificationid_key";
        private static readonly int NotificationIdDefault = 0;
        #endregion

        public static int NotificationId
        {
            get { return AppSettings.GetValueOrDefault<int>(NotificationIdKey, NotificationIdDefault); }
            set { AppSettings.AddOrUpdateValue<int>(NotificationIdKey, value); }
        }

        public static int GetUniqueNotificationId()
        {
            return NotificationId++;
        }
    }
}