﻿using EquilibriumApp.Models;
using Firebase.Database.Query;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

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
            FiltroVisivel = false;
            FiltrarCommand = new Command(() => FiltroVisivel = !FiltroVisivel );
        }

        public ObservableCollection<Atividades> Atividades { get; set; }
        public ObservableCollection<Atividades> AtividadesFiltradas {get; set;}
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
                    foreach(var i in Atividades)
                    {
                        AtividadesFiltradas.Add(i);
                    }
                }
                else
                {
                    AtividadesFiltradas.Clear();

                    var result = Atividades.Where(x => x.Name.Contains(filtroSelecionado));

                    AtividadesFiltradas.Clear();
                    foreach (var i in result)
                    {
                        AtividadesFiltradas.Add(i);
                    }
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
                    Atividades.Add(new Atividades
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

            foreach (var i in Atividades)
                AtividadesFiltradas.Add(i);

            await base.InitializeAsync(navigationData);
        }


        private async Task DetalharAtividade()
        {
            await DialogService.ShowMessage("Em Desenvolvimento", "Aviso");
        }
    }
}
