using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace EquilibriumApp.ViewModels
{
    public class SelecaoDeInteressesViewModel : BaseViewModel
    {
        public SelecaoDeInteressesViewModel()
        {
            Title = "Seleção de Interesses";
        }

        public async override Task InitializeAsync(object navigationData)
        {
            var result = Firebase.Child("users").OrderByKey();
            Console.WriteLine("test");

            await base.InitializeAsync(navigationData);
        }
    }
}
