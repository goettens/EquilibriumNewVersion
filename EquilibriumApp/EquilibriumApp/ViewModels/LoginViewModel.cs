using EquilibriumApp.Models;
using EquilibriumApp.Services;
using EquilibriumApp.Services.Common;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;

namespace EquilibriumApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
            CadastrarCommand = new Command(async () => await NavigationService.NavigateToAsync<CadastroViewModel>());
            LoginCommand = new Command(async () => await FazerLogin());
        }

        public User UsuarioSelecionado;

        public ICommand CadastrarCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        
        public ICommand AceitarUsuarioCmd { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public async Task FazerLogin()
        {
            if(CheckValidations())
            {
                try
                {
                    var auth = DependencyService.Get<IFirebaseAuthenticator>();
                    string token = await auth.LoginWithEmailPassword(Email, Password);
                    if (!string.IsNullOrEmpty(token))
                    {
                        var firebase = new FirebaseClient(@"https://roda-da-vida-app.firebaseio.com/", new FirebaseOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(token)
                        });

                        var comentarios = await firebase.Child("comments").Child("-LOz2J7AEMS5MS4Ce2lf").OrderByKey().OnceAsync<Comment>();
                        foreach(var i in comentarios)
                        {
                            string a = i.ToString();
                        }
                        await NavigationService.NavigateToAsync<SelecaoDeInteressesViewModel>();
                    }
                    else
                        await DialogService.ShowMessage("Error", "Error");
                }
                catch(Exception e)
                {
                    await DialogService.ShowMessage("Error", e.ToString());
                }
            }

        }
        private bool CheckValidations()
        {
            if (string.IsNullOrEmpty(Email))
            {
                DialogService.ShowMessage("Digite o login", "Campo Obrigátório");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {

                DialogService.ShowMessage("Digite a senha", "Campo Obrigátório");
                return false;
            }
            return true;
        }

    }
}
