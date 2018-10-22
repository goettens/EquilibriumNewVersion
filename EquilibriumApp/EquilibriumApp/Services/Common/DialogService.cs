using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EquilibriumApp.Services.Common
{
    public class DialogService : IDialogService
    {
        public async Task ShowMessage(string message, string title)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, buttonText);
            afterHideCallback?.Invoke();
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            var result = await Application.Current.MainPage.DisplayAlert(title, message, buttonConfirmText, buttonCancelText);
            afterHideCallback?.Invoke(result);
            return result;
        }

        public async Task ShowMessageBox(string message, string title, string v)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}