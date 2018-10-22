using EquilibriumApp.Services;
using EquilibriumApp.Services.Common;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDevice = Xamarin.Forms.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EquilibriumApp
{
    public partial class App : Application
    {
        public string AuthToken { get; set; }
        public bool Authenticated { get; set; } = false;

        public App()
        {
            if (XamarinDevice.RuntimePlatform == XamarinDevice.iOS || XamarinDevice.RuntimePlatform == XamarinDevice.Android)
            {
                var localize = DependencyService.Get<ILocalize>();
                // determine the correct, supported .NET culture
                var ci = localize.GetCurrentCultureInfo();
                localize.SetLocale(ci); // set the Thread for locale-aware methods
            }
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitNavigation();
        }

        private async Task InitNavigation()
        {
            var navigationService = AppContainer.Instance.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }
    }
}
