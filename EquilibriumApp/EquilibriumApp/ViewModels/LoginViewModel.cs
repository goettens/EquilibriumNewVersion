using EquilibriumApp.Models;
using EquilibriumApp.Services.Common.Authentication;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Database.Query;

namespace EquilibriumApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
            CadastrarCommand = new Command(async () => await NavigationService.NavigateToAsync<CadastroViewModel>());
            RecuperarSenhaCommand = new Command(async () => await NavigationService.NavigateToAsync<RecuperarSenhaViewModel>());
            LoginCommand = new Command(async () => await FazerLogin());
            LembrarMe = SettingsService.LembrarMe;
        }

        public User UsuarioSelecionado;

        public ICommand CadastrarCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand AceitarUsuarioCmd { get; set; }
        public ICommand RecuperarSenhaCommand { get; set; }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetProperty(ref email, value, nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value, nameof(Password));
            }
        }

        private bool lembrarMe;
        public bool LembrarMe
        {
            get
            {
                return lembrarMe;
            }
            set
            {
                SettingsService.LembrarMe = value;
                SetProperty(ref lembrarMe, value, nameof(LembrarMe));
            }
        }

        public override Task InitializeAsync(object navigationData)
        {

            Email = SettingsService.LembrarMe ? SettingsService.Email : "";
            Password = SettingsService.LembrarMe ? SettingsService.Password : "";
            return base.InitializeAsync(navigationData);
        }

        public async Task FazerLogin()
        {
            if(CheckValidations())
            {
                try
                {
                    var auth = DependencyService.Get<IFirebaseAuthenticator>();
                    SettingsService.AccessToken = null;
                    SettingsService.AccessToken = await auth.LoginWithEmailPassword(Email, Password);
                    if (!string.IsNullOrEmpty(SettingsService.AccessToken))
                    {
                        SettingsService.Email = SettingsService.LembrarMe ? Email : null;
                        SettingsService.Password = SettingsService.LembrarMe ? Password : null;

                        SetFirebase();
                        var comentarios = await Firebase.Child("comments").Child("-LOz2J7AEMS5MS4Ce2lf").OrderByKey().OnceAsync<Comment>();

                        await NavigationService.NavigateToAsync<SelecaoDeInteressesViewModel>();
                    }
                    else
                        await DialogService.ShowMessage("Usuário não cadastrado", "Erro");
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
