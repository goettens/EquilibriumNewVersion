using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;

namespace EquilibriumApp.Services.Common
{
    class SettingsService : ISettingsService
    {
        public SettingsService()
        {

        }
        public string DefaultAPIUrl
        {
            get
            {
                return @"https://roda-da-vida-app.firebaseio.com/";
            }
        }

        public string AccessToken
        {
            get
            {
                return Preferences.Get(nameof(AccessToken), null);
            }
            set
            {
                Preferences.Set(nameof(AccessToken), value);
            }
        }
        
        public string UserName
        {
            get
            {
                return Preferences.Get(nameof(UserName), null);
            }
            set
            {
                Preferences.Set(nameof(UserName), value);
            }
        }
        public string Email
        {
            get
            {
                return Preferences.Get(nameof(Email), null);
            }
            set
            {
                Preferences.Set(nameof(Email), value);
            }
        }

        public bool LembrarMe
        {
            get
            {
                return Preferences.Get(nameof(LembrarMe), false);
            }
            set
            {
                Preferences.Set(nameof(LembrarMe), value);
            }
        }
    }
}
