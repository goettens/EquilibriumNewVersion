using EquilibriumApp.Models;
using EquilibriumApp.Services;
using EquilibriumApp.Services.Common;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            SetFirebase();
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

        ObservableCollection<Categoria> Categorias;
        public async Task SetAreasAsync()
        {
            Categorias = new ObservableCollection<Categoria>();
            try
            {
                var result = await Firebase.Child("area").OrderByKey().OnceAsync<Categoria>();

                foreach (var r in result)
                {
                    List<Subcategories> subs = new List<Subcategories>();

                    var intern = await Firebase.Child("area").Child(r.Key).Child("subcategories").OrderByKey().OnceAsync<Subcategories>();

                    foreach (var i in intern)
                    {
                        subs.Add(new Subcategories()
                        {
                            Id = i.Key,
                            Name = i.Object.Name,
                            Description = i.Object.Description,
                            Enumerator = i.Object.Enumerator
                        });
                    }

                    Categorias.Add(new Categoria()
                    {
                        Description = r.Object.Description,
                        Id = r.Key,
                        Name = r.Object.Name,
                        Subs = subs
                    });
                }
            }
            catch (Exception Ex)
            {
                await DialogService.ShowMessage("Não foi possível recuperar as categorias de interesse", "Erro");
                //await NavigationService.NavigateToPrevious();
            }
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
