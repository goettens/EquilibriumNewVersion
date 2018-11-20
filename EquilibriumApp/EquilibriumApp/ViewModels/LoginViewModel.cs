using EquilibriumApp.Models;
using EquilibriumApp.Services.Common.Authentication;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Database.Query;
using System.Linq;
using System.Collections.Generic;

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
            SettingsService.IdUserAtual = null;
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
                        SettingsService.Email = Email;

                        SetFirebase();
                        //var comentarios = await Firebase.Child("comments").Child("-LOz2J7AEMS5MS4Ce2lf").OrderByKey().OnceAsync<Comment>();

                        var result = await Firebase.Child("users").OrderByKey().OnceAsync<User>();

                        foreach(var user in result)
                        {
                            if(user.Object.Email == SettingsService.Email)
                            {
                                SettingsService.IdUserAtual = user.Key;
                                SettingsService.UserName = user.Object.Name;
                                SettingsService.Email = user.Object.Email;
                            }
                        }

                        await SetAreasAsync();

                        await NavigationService.NavigateToAsync<SelecaoDeInteressesViewModel>();
                    }
                }
                catch(Exception e)
                {
                    await DialogService.ShowMessage("Usuário ou Senha incorretos", "Erro");
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
