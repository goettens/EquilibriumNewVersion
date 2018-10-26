﻿using EquilibriumApp.Services;
using EquilibriumApp.Services.Common;
using Firebase.Database;
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
            DialogService = AppContainer.Instance.Resolve<IDialogService>();
            SettingsService = AppContainer.Instance.Resolve<ISettingsService>();
        }

        public INavigationService NavigationService { get; }
        public IDialogService DialogService { get; }
        public ISettingsService SettingsService { get; set; }

        public FirebaseClient Firebase { get; set; }

        public bool SetFirebase()
        {
            try
            {
                Firebase = new FirebaseClient(SettingsService.DefaultAPIUrl, new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(SettingsService.AccessToken)
                });
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
                DialogService.ShowMessage("Não foi possível fazer o login", "Erro");
            }

            return Firebase != null ? true : false;
        }


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
