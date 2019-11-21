using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SwapShuffle.Helper
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public class UserSettings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        //private const string SettingsKey = "settings_key";
        //private static readonly string SettingsDefault = string.Empty;

        #endregion

        //demo

        //public static string GeneralSettings
        //{
        //    get
        //    {
        //        return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
        //    }
        //    set
        //    {
        //        AppSettings.AddOrUpdateValue(SettingsKey, value);
        //    }
        //}



        #region Setting Constants

        private const string UidKey = "Uid";
        private static readonly long uidDefault = 0;

        #endregion


        public static long Uid
        {
            get
            {
                return AppSettings.GetValueOrDefault(UidKey, uidDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UidKey, value);
            }
        }

    }
}
