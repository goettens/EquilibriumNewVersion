using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EquilibriumApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Adicionar : ContentPage
	{
		public Adicionar ()
		{
			InitializeComponent ();
		}

        private async void Alert(object sender, EventArgs args)
        {
            await DisplayAlert("Teste Sucess", "Sucesso", "Ok");
        }
    }
}