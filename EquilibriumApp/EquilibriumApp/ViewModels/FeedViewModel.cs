using EquilibriumApp.Models;
using Firebase.Database.Query;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using EquilibriumApp.Classes;

namespace EquilibriumApp.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        public FeedViewModel()
        {
            Title = "Feed";
            Atividades = new ObservableRangeCollection<Atividades>();
            AtividadesFiltradas = new ObservableRangeCollection<Atividades>();
            TodasAtividades = new ObservableRangeCollection<Atividades>();
            DetalharAtividadeCommand = new Command(async () => await DetalharAtividade());
            FiltroVisivel = false;
            FiltrarCommand = new Command(() => FiltroVisivel = !FiltroVisivel );
        }

        public ObservableRangeCollection<Atividades> Atividades { get; set; }
        public ObservableRangeCollection<Atividades> AtividadesFiltradas {get; set;}
        public ObservableRangeCollection<Atividades> TodasAtividades { get; set;}

        public ICommand DetalharAtividadeCommand { get; set; }
        public ICommand FiltrarCommand { get; set; }

        #region Filtro
        private bool filtroVisivel;
        public bool FiltroVisivel
        {
            get
            {
                return filtroVisivel;
            }
            set
            {
                SetProperty(ref filtroVisivel, value, nameof(FiltroVisivel));
            }
        }
        private string filtroSelecionado = "";
        public string FiltroSelecionado
        {
            get
            {
                return filtroSelecionado;
            }
            set
            {
                SetProperty(ref filtroSelecionado, value, nameof(FiltroSelecionado));

                if (string.IsNullOrEmpty(filtroSelecionado))
                {
                    AtividadesFiltradas.Clear();
                    AtividadesFiltradas.AddRange(Atividades);
                }
                else
                {
                    AtividadesFiltradas.Clear();

                    var result = Atividades.Where(x => x.Name.Contains(filtroSelecionado));

                    AtividadesFiltradas.Clear();
                    AtividadesFiltradas.AddRange(result);
                }
            }
        }
        #endregion
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

        public override async Task InitializeAsync(object navigationData)
        {

            SetFirebase();
            try
            {
                var result = await Firebase.Child("recommendedactivities").OrderByKey().OnceAsync<Atividades>();
                foreach (var i in result)
                {
                    TodasAtividades.Add(new Atividades
                    {
                        Id = i.Key,
                        EnumCategories = i.Object.EnumCategories,
                        ImageURL = i.Object.ImageURL,
                        Likes = i.Object.Likes,
                        Link = i.Object.Link,
                        LongDescription = i.Object.LongDescription,
                        MaximumPrice = i.Object.MaximumPrice,
                        MinimumPrice = i.Object.MinimumPrice,
                        Name = i.Object.Name,
                        ReportsCount = i.Object.ReportsCount
                    });
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowMessage("Não foi possível buscar atividades. Tente mais tarde", "error");
            }

            SetCategorias(SettingsService.EnumCategorias, TodasAtividades.ToList());

            AtividadesFiltradas.AddRange(Atividades);

            await base.InitializeAsync(navigationData);
        }


        private async Task DetalharAtividade()
        {
            await DialogService.ShowMessage("Em Desenvolvimento", "Aviso");
        }

        public void SetCategorias(int Categorias, List<Atividades> TodasAtividades)
        {
            if (Categorias == 0)
            {
                return;
            }
            else if (Categorias >= 2048)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 2048));
                SetCategorias(Categorias - 2048, TodasAtividades);
            }
            else if (Categorias >= 1024)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 1024));
                SetCategorias(Categorias - 1024, TodasAtividades);
            }
            else if (Categorias >= 512)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 1024));
                SetCategorias(Categorias - 512, TodasAtividades);

            }
            else if (Categorias >= 256)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 256));
                SetCategorias(Categorias - 256, TodasAtividades);

            }
            else if (Categorias >= 128)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 128));
                SetCategorias(Categorias - 128, TodasAtividades);

            }
            else if (Categorias >= 64)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 64));
                SetCategorias(Categorias - 64, TodasAtividades);

            }
            else if (Categorias >= 32)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 32));
                SetCategorias(Categorias - 32, TodasAtividades);

            }
            else if (Categorias >= 16)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 16));
                SetCategorias(Categorias - 16, TodasAtividades);

            }
            else if (Categorias >= 8)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 8));
                SetCategorias(Categorias - 8, TodasAtividades);

            }
            else if (Categorias >= 4)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 4));
                SetCategorias(Categorias - 4, TodasAtividades);

            }
            else if (Categorias >= 2)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 2));
                SetCategorias(Categorias - 2, TodasAtividades);

            }
            else if (Categorias >= 1)
            {
                Atividades.AddRange(TodasAtividades.Where(x => x.EnumCategories >= 1));
                SetCategorias(Categorias - 1, TodasAtividades);

            }
        }
    }
}
