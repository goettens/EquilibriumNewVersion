using EquilibriumApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EquilibriumApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
            CadastrarCommand = new Command(async () => await NavigationService.NavigateToAsync<CadastroViewModel>());
        }

        public ICommand CadastrarCommand { get; set; }
        public User UsuarioSelecionado;
        public ICommand AceitarUsuarioCmd { get; set; }
    }
}
