using EquilibriumApp.ViewModels;
using System.Threading.Tasks;

namespace EquilibriumApp.Services.Common
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task NavigateToPrevious();

        BaseViewModel PreviousPageViewModel { get; }
    }
}
