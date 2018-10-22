using System;
using System.Threading.Tasks;

namespace EquilibriumApp.Services.Common
{
    public interface IDialogService
    {
        Task ShowMessage(string message, string title);
        Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback);
        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback);
    }
}