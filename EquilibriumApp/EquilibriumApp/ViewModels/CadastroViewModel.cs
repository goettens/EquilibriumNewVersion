using EquilibriumApp.Models;
using EquilibriumApp.Services.Common.Authentication;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Database.Query;

namespace EquilibriumApp.ViewModels
{
    public class CadastroViewModel : BaseViewModel
    {
        public CadastroViewModel()
        {
            Title = "Cadastrar";
            VerificaCadastro();
            CadastrarCommand = new Command(async () => await Cadastrar());
        }

        

        public ICommand CadastrarCommand { get; set; }

        private string name;

        public string Name
        {
            get { return name; }
            set {
                SetProperty(ref name, value, nameof(Name));
                VerificaCadastro();
            }
        }


        private string email;

        public string Email
        {
            get { return email; }
            set {
                SetProperty(ref email, value, nameof(Email));
                VerificaCadastro();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set {
                SetProperty(ref password, value, nameof(Password));
                VerificaCadastro();
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set {
                SetProperty(ref confirmPassword, value, nameof(ConfirmPassword));
                VerificaCadastro();
            }
        }

        private string warning;

        public string Warning
        {
            get { return warning; }
            set { SetProperty(ref warning, value, nameof(Warning)); }
        }

        private bool cadastrarAtivo;

        public bool CadastrarAtivo
        {
            get { return cadastrarAtivo; }
            set { SetProperty(ref cadastrarAtivo, value, nameof(CadastrarAtivo)); }
        }

        private void VerificaCadastro()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                CadastrarAtivo = false;
                Warning = "Todos os Campos devem ser preenchidos";
            }
            else if (Password != ConfirmPassword)
            {
                CadastrarAtivo = false;
                Warning = "As senhas digitadas devem ser iguais";
            }
            else
            {
                CadastrarAtivo = true;
                Warning = "";
            }
        }

        private async Task Cadastrar()
        {
            bool cadastrado = false;
            if (CadastrarAtivo)
            {
                try
                {
                    var auth = DependencyService.Get<IFirebaseAuthenticator>();
                    cadastrado = await auth.CreateLogin(Email, Password);
                }
                catch
                {
                    await DialogService.ShowMessage("Email já cadastrado", "Erro");
                }

                if (cadastrado)
                {
                    var auth = DependencyService.Get<IFirebaseAuthenticator>();
                    SettingsService.AccessToken = null;
                    SettingsService.AccessToken = await auth.LoginWithEmailPassword(Email, Password);
                    if (!string.IsNullOrEmpty(SettingsService.AccessToken))
                    {
                        SetFirebase();
                        try
                        {
                            var usuario = await Firebase.Child("users").PostAsync(new User(Name.ToUpper(), Email, Password, 0, ""));
                            SettingsService.AccessToken = null;
                            SettingsService.Email = null;
                            SettingsService.UserName = null;
                            await DialogService.ShowMessage("Usuário cadastrado. Faça o login", "Sucesso");
                        }
                        catch
                        {
                            await DialogService.ShowMessage("Não foi possível cadastrar o usuário", "Erro no Servidor");
                        }
                    }
                }
                else
                {
                    await DialogService.ShowMessage("Email já cadastrado", "Erro");
                }
            }
        }
    }
}
