using EquilibriumApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EquilibriumApp.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        public FeedViewModel()
        {
            Title = "Feed";
            Atividades = new ObservableCollection<Atividades>();
            AtividadesFiltradas = new ObservableCollection<Atividades>();
            DetalharAtividadeCommand = new Command(async () => await DetalharAtividade());
        }

        public ObservableCollection<Atividades> Atividades { get; set; }
        public ObservableCollection<Atividades> AtividadesFiltradas {get; set;}
        public ICommand DetalharAtividadeCommand { get; set; }

        private Atividades atividadeSelecionada;
        public Atividades AtividadeSelecionada
        {
            get
            {
                return atividadeSelecionada;
            }
            set
            {
                SetProperty(ref atividadeSelecionada, value, nameof(AtividadeSelecionada));
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            Atividades atv;
            SetFirebase();
            try
            {
                var result = await Firebase.Child("recommendedactivities").OrderByKey().OnceAsync<Atividades>();
                foreach (var i in result)
                {
                    atv = new Atividades
                    {
                        
                    };

                }


                SetCategorias(Usuario.EnumCategories);

            }
            catch (Exception ex)
            {
                await DialogService.ShowMessage(ex.ToString(), "error");
            }


            return base.InitializeAsync(navigationData);
        }


        private async Task DetalharAtividade()
        {
            await DialogService.ShowMessage("Em Desenvolvimento", "Aviso");
        }
    }
}
