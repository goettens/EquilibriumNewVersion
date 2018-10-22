using EquilibriumApp.Services;
using EquilibriumApp.Services.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EquilibriumApp.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        public BaseViewModel()
        {
            NavigationService = AppContainer.Instance.Resolve<INavigationService>();
        }

        public INavigationService NavigationService { get; }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (value != isBusy)
                {
                    isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                }
            }
        }

        private string busyMessage = "Carregando";
        public string BusyMessage
        {
            get
            {
                return busyMessage;
            }
            set
            {
                SetProperty(ref busyMessage, value ?? "Carregando", nameof(BusyMessage));
            }
        }


        public bool isValid;
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                SetProperty(ref isValid, value, nameof(IsValid));
            }
        }

        string title = string.Empty;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty(ref title, value, nameof(Title));
            }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        protected bool SetProperty<T>(ref T backingStore, T value, string propertyName, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
