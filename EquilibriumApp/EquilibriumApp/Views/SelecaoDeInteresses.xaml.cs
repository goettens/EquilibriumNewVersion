using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EquilibriumApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelecaoDeInteresses : ContentPage
	{
		public SelecaoDeInteresses ()
		{
			InitializeComponent ();
		}

        private async void Alert(object sender, EventArgs args)
        {
            await DisplayAlert("Teste Sucess", "Sucesso", "Ok");
        }

    }
}