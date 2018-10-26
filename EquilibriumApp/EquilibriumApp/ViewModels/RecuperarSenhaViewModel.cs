using EquilibriumApp.Services.Common.Authentication;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EquilibriumApp.ViewModels
{
    public class RecuperarSenhaViewModel : BaseViewModel
    {
        public RecuperarSenhaViewModel()
        {
            Title = "Recuperar Senha";
            RecuperarCommand = new Command(async () => await Recuperar());
        }

        public ICommand RecuperarCommand { get; set; }
        public string email;
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

        public async override Task InitializeAsync(object navigationData)
        {
            Email = navigationData as string;
            await base.InitializeAsync(navigationData);
        }

        public async Task Recuperar()
        {
            var result = DependencyService.Get<IFirebaseAuthenticator>();
            bool sucess = await result.ChangePassword(Email);
            if (sucess)
                await DialogService.ShowMessage(string.Format("Email enviado para {0}", Email), "Sucesso");
            else
                await DialogService.ShowMessage(string.Format("{0} não cadastrado" , Email), "Erro");
        }
    }
}
