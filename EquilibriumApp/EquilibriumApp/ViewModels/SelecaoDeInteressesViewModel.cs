using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquilibriumApp.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace EquilibriumApp.ViewModels
{
    public class SelecaoDeInteressesViewModel : BaseViewModel
    {
        public SelecaoDeInteressesViewModel()
        {
            Title = "Seleção de Interesses";
            Categorias = new List<Categoria>();
        }

        public List<Categoria> Categorias { get; set; }

        #region CATEGORIAS BOOLEAN
        private bool saude;
        public bool Saude
        {
            get { return saude; }
            set { SetProperty(ref saude, value, nameof(Saude)); }
        }

        private bool desenvolvimento;
        public bool Desenvolvimento
        {
            get { return desenvolvimento; }
            set { SetProperty(ref desenvolvimento, value, nameof(Desenvolvimento)); }
        }

        private bool equilibrio;
        public bool Equilibrio
        {
            get { return equilibrio; }
            set { SetProperty(ref equilibrio, value, nameof(Equilibrio)); }
        }

        private bool realizacao;
        public bool Realizacao
        {
            get { return realizacao; }
            set { SetProperty(ref realizacao, value, nameof(Realizacao)); }
        }

        private bool recursos;
        public bool Recursos
        {
            get { return recursos; }
            set { SetProperty(ref recursos, value, nameof(Recursos)); }
        }

        private bool contribuicao;
        public bool Contribuicao
        {
            get { return contribuicao; }
            set { SetProperty(ref contribuicao, value, nameof(Contribuicao)); }
        }

        private bool relacionamentoFamila;
        public bool RelacionamentoFamilia
        {
            get { return relacionamentoFamila; }
            set { SetProperty(ref relacionamentoFamila, value, nameof(RelacionamentoFamilia)); }
        }

        private bool relacionamentoAmoroso;
        public bool RelacionamentoAmoroso
        {
            get { return relacionamentoAmoroso; }
            set { SetProperty(ref relacionamentoAmoroso, value, nameof(RelacionamentoAmoroso)); }
        }

        private bool vidaSocial;
        public bool VidaSocial
        {
            get { return vidaSocial; }
            set { SetProperty(ref vidaSocial, value, nameof(VidaSocial)); }
        }

        private bool criatividade;
        public bool Criatividade
        {
            get { return criatividade; }
            set { SetProperty(ref criatividade, value, nameof(Criatividade)); }
        }

        private bool praticas;
        public bool Praticas
        {
            get { return saude; }
            set { SetProperty(ref saude, value, nameof(Saude)); }
        }

        private bool plenitude;
        public bool Plenitude
        {
            get { return plenitude; }
            set { SetProperty(ref plenitude, value, nameof(Plenitude)); }
        }

        #endregion

        public override async Task InitializeAsync(object navigationData)
        {
            SetFirebase();
            try
            {
                var result = await Firebase.Child("area").OrderByKey().OnceAsync<Categoria>();

                //var resultds = await Firebase.Child("area").Child("001").Child("subcategories").OrderByKey().OnceAsync<Subcategories>();
                
                foreach (var r in result)
                {
                    List<Subcategories> subs = new List<Subcategories>();

                    var intern = await Firebase.Child("area").Child(r.Key).Child("subcategories").OrderByKey().OnceAsync<Subcategories>();

                    foreach(var i in intern)
                    {
                        subs.Add(new Subcategories()
                        {
                            Id = i.Key,
                            Name = i.Object.Name,
                            Description = i.Object.Description,
                            Enumerator = i.Object.Enumerator
                        });
                    }

                    Categorias.Add(new Categoria()
                    {
                        Description = r.Object.Description,
                        Id = r.Key,
                        Name = r.Object.Name,
                        Subs = subs
                    });
                }
            }
            catch(Exception ex)
            {
                await DialogService.ShowMessage("Não foi possível recuperar as categorias de interesse", "Erro");
                //await NavigationService.NavigateToPrevious();
            }

            

            await base.InitializeAsync(navigationData);
        }
    }
}
