using EquilibriumApp.ViewModels;
using EquilibriumApp.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EquilibriumApp.Services.Common
{
    public class NavigationService : INavigationService
    {
        ISettingsService SettingsService;
        public NavigationService()
        {
            SettingsService = AppContainer.Instance.Resolve<ISettingsService>();
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<LoginViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is Login || page is SelecaoDeInteresses)
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
            else if(Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                throw new Exception("A página inicial não foi definida");
            }

            AppContainer.Instance.LoadViewModel(page);

            if (page.BindingContext is BaseViewModel viewModel)
            {
                viewModel.IsBusy = true;
                try
                {
                    await viewModel.InitializeAsync(parameter);
                }
                finally
                {
                    viewModel.IsBusy = false;
                }
            }
        }

        //Views are in the same assembly as view model types.
        //Views are in a.Views child namespace.
        //View models are in a.ViewModels child namespace.
        //View names correspond to view model names, with "ViewModel" removed.

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.Namespace.Replace("ViewModels", "Views") + '.' + viewModelType.Name.Replace("ViewModel", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = AppContainer.Instance.Resolve(pageType) as Page;
            return page;
        }

        public async Task NavigateToPrevious()
        {
            var navigationPage = Application.Current.MainPage as INavigation;

            await navigationPage.PopAsync();
        }

        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                var master = Application.Current.MainPage as Cadastro; //TODO: Editar para o Feed
                var viewModel = master.Navigation.NavigationStack[master.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }
    }
}
