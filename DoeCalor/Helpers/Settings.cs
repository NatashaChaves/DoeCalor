using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoeCalor.Helpers
{
   
        public static class Settings
        {
            private static ISettings AppSettings
            {
                get
                {
                    return CrossSettings.Current;
                }
            }

            #region Setting Constants

            private const string SettingsKey = "settings_key";
            private static readonly string SettingsDefault = string.Empty;

            private const string EmailKey = "email_key";
            private static readonly string EmailDefault = string.Empty;

            private const string NameKey = "name_key";
            private static readonly string NameDefault = string.Empty;

            private const string UrlPhotoKey = "url_photo_key";
            private static readonly string UrlPhotoDefault = string.Empty;

            #endregion


            public static string GeneralSettings
            {
                get
                {
                    return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(SettingsKey, value);
                }
            }

            public static string Email
            {
                get
                {
                    return AppSettings.GetValueOrDefault(EmailKey, EmailDefault);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(EmailKey, value);
                }
            }

            public static string Name
            {
                get
                {
                    return AppSettings.GetValueOrDefault(NameKey, NameDefault);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(NameKey, value);
                }
            }

            public static string UrlPhoto
            {
                get
                {
                    return AppSettings.GetValueOrDefault(UrlPhotoKey, UrlPhotoDefault);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(UrlPhotoKey, value);
                }
            }
        }
    }
