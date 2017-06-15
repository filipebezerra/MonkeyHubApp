// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MonkeyHubApp.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings { get { return CrossSettings.Current; } }

        #region Setting Constants
        private const string UserIdKey = "userid_key";
        private static readonly string UserIdDefault = string.Empty;

        private const string AuthTokenKey = "authtoken_key";
        private static readonly string AuthTokenDefault = string.Empty;
        #endregion

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserIdKey, value); }
        }

        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value); }
        }

        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);
    }
}