using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EquilibriumApp.Services.Common;
using Xamarin.Forms;

[assembly: Dependency(typeof(EquilibriumApp.Droid.Services.AndroidLocalize))]
namespace EquilibriumApp.Droid.Services
{
    class AndroidLocalize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace('_', '-');
            CultureInfo ci = null;
            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                ci = new CultureInfo("en");
            }
            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

    }
}