using Autofac;
using Autofac.Util;
using EquilibriumApp.Services.Common;
using EquilibriumApp.ViewModels;
using EquilibriumApp.Views;
using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace EquilibriumApp.Services
{
    public class AppContainer
    {
        private static readonly Lazy<AppContainer> _appContainer = new Lazy<AppContainer>(() => new AppContainer());
        public static AppContainer Instance => _appContainer.Value;
        private IContainer _container;

        public AppContainer()
        {
            var builder = new ContainerBuilder();
            LoadServices(builder);
            LoadViewModels(builder);
            LoadViews(builder);

            _container = builder.Build();
        }

        private void LoadViewModels(ContainerBuilder builder)
        {
            var assembly = typeof(BaseViewModel).GetTypeInfo().Assembly;
            var viewModelTypes = assembly.GetLoadableTypes()
                                .Where(x => x.IsAssignableTo<BaseViewModel>() && x != typeof(BaseViewModel));
            foreach (var type in viewModelTypes)
            {
                builder.RegisterType(type).AsSelf();
            }
        }

        private void LoadViews(ContainerBuilder builder)
        {
            var assembly = typeof(Login).GetTypeInfo().Assembly;
            var viewTypes = assembly.GetLoadableTypes()
                                .Where(x => x.IsAssignableTo<ContentPage>() && x != typeof(ContentPage));
            foreach (var type in viewTypes)
            {
                builder.RegisterType(type).AsSelf();
            }
        }

        private void LoadServices(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();

            var settings = new SettingsService();
            builder.Register(c => settings).As<ISettingsService>().SingleInstance();
        }

        public void LoadViewModel(BindableObject view)
        {
            Type viewType = view.GetType();
            string @namespace = viewType.Namespace.Replace(".Views", ".ViewModels");
            string className = viewType.Name + "ViewModel";
            string assemblyName = viewType.GetTypeInfo().Assembly.FullName;
            string fullName = $"{@namespace}.{className}, {assemblyName}";

            var viewModelType = Type.GetType(fullName);
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
