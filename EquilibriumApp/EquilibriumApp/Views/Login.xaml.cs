using EquilibriumApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Autofac;
using EquilibriumApp.ViewModels;

namespace EquilibriumApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent();
            //this.BindingContext = new LoginViewModel();
		}

        async void Login_Cicked(object sender, EventArgs e)
        {

            if (CheckValidations())
            {
                var auth = DependencyService.Get<IFirebaseAuthenticator>();
                string token = await auth.LoginWithEmailPassword(txtEmail.Text, txtPassword.Text);
                await DisplayAlert("Token", token, "Ok");
            }
        }
        private bool CheckValidations()
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                DisplayAlert("Alert", "Enter email", "ok");
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {

                DisplayAlert("Alert", "Enter password", "ok");
                return false;
            }
            return true;
        }

        public async void Alert(object sender, EventArgs args)
        {
            await DisplayAlert("Ola", "Rolá", "Noix");
        }
    }
}