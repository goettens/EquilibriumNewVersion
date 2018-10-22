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
		}

        public async void Alert(object sender, EventArgs args)
        {
            await DisplayAlert("Ola", "Rolá", "Noix");
        }
    }
}