using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using EquilibriumApp.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace EquilibriumApp.ViewModels
{
    public class SelecaoDeInteressesViewModel : BaseViewModel
    {
        public SelecaoDeInteressesViewModel()
        {
            Title = "Seleção de Interesses";
            Categorias = new List<Categoria>();
            Usuario = new User();
            SalvarCommand = new Command(async() => await Salvar());
            NumCat = 0;
        }

        public User Usuario { get; set; }
        public List<Categoria> Categorias { get; set; }
        public ICommand SalvarCommand { get; set; }


        private int numCat;
        public int NumCat
        {
            get { return numCat; }
            set { SetProperty(ref numCat, value, nameof(NumCat)); }
        }

        #region CATEGORIAS BOOLEAN
        private bool saude;
        public bool Saude
        {
            get { return saude; }
            set { SetProperty(ref saude, value, nameof(Saude));
                NumCat = value ? NumCat + 1 : NumCat - 1;
            }
        }

        private bool desenvolvimento;
        public bool Desenvolvimento
        {
            get { return desenvolvimento; }
            set { SetProperty(ref desenvolvimento, value, nameof(Desenvolvimento));
                NumCat = value ? NumCat + 2 : NumCat - 2;
            }
        }

        private bool equilibrio;
        public bool Equilibrio
        {
            get { return equilibrio; }
            set { SetProperty(ref equilibrio, value, nameof(Equilibrio));
                NumCat = value ? NumCat + 4 : NumCat - 4;
            }
        }

        private bool realizacao;
        public bool Realizacao
        {
            get { return realizacao; }
            set { SetProperty(ref realizacao, value, nameof(Realizacao));
                NumCat = value ? NumCat + 8 : NumCat - 8;
            }
        }

        private bool recursos;
        public bool Recursos
        {
            get { return recursos; }
            set { SetProperty(ref recursos, value, nameof(Recursos));
                NumCat = value ? NumCat + 16 : NumCat - 16;
            }
        }

        private bool contribuicao;
        public bool Contribuicao
        {
            get { return contribuicao; }
            set { SetProperty(ref contribuicao, value, nameof(Contribuicao));
                NumCat = contribuicao ? NumCat + 32 : NumCat - 32;
            }
        }

        private bool relacionamentoFamila;
        public bool RelacionamentoFamilia
        {
            get { return relacionamentoFamila; }
            set { SetProperty(ref relacionamentoFamila, value, nameof(RelacionamentoFamilia));
                NumCat = value ? NumCat + 64 : NumCat - 64;
            }
        }

        private bool relacionamentoAmoroso;
        public bool RelacionamentoAmoroso
        {
            get { return relacionamentoAmoroso; }
            set { SetProperty(ref relacionamentoAmoroso, value, nameof(RelacionamentoAmoroso));
                NumCat = value ? NumCat + 128 : NumCat - 128;
            }
        }

        private bool vidaSocial;
        public bool VidaSocial
        {
            get { return vidaSocial; }
            set { SetProperty(ref vidaSocial, value, nameof(VidaSocial));
                NumCat = value ? NumCat + 256 : NumCat - 256;
            }
        }

        private bool criatividade;
        public bool Criatividade
        {
            get { return criatividade; }
            set { SetProperty(ref criatividade, value, nameof(Criatividade));
                NumCat = value ? NumCat + 512 : NumCat - 512;
            }
        }

        private bool praticas;
        public bool Praticas
        {
            get { return praticas; }
            set { SetProperty(ref praticas, value, nameof(Praticas));
                NumCat = value ? NumCat + 1024 : NumCat - 1024;
            }
        }

        private bool plenitude;
        public bool Plenitude
        {
            get { return plenitude; }
            set { SetProperty(ref plenitude, value, nameof(Plenitude));
                NumCat = value ? NumCat + 2048 : NumCat - 2048;
            }
        }

        #endregion

        public override async Task InitializeAsync(object navigationData)
        {
            SetFirebase();
            try
            {
                var result = await Firebase.Child("users").OrderByKey().StartAt(SettingsService.IdUserAtual).LimitToFirst(1).OnceAsync<User>();
                foreach (var i in result)
                    Usuario = new User(i.Object.Email, i.Object.EnumCategories, i.Object.ImageURL, i.Object.Name);

                SetCategorias(Usuario.EnumCategories);

            }
            catch(Exception ex)
            {
               await DialogService.ShowMessage(ex.ToString(), "error");
            }

            await base.InitializeAsync(navigationData);
        }

        public async Task Salvar()
        {
            await Firebase.Child("users").Child(SettingsService.IdUserAtual).Child("EnumCategories").PutAsync(NumCat);
            try
            {

            await NavigationService.NavigateToAsync<FeedViewModel>();
            }
            catch(Exception ex)
            {
                await DialogService.ShowMessage(ex.ToString(), "erro");
            }
        }

        /// <summary>
        /// XUNCHO recursivo pra fazer funcionar o esquema das categorias.
        /// </summary>
        /// <param name="Categorias"></param>
        public void SetCategorias(int Categorias)
        {
            if (Categorias == 0)
            {
                return;
            }
            else if (Categorias >= 2048)
            {
                Plenitude = true;
                SetCategorias(Categorias - 2048);
            }
            else if (Categorias >= 1024)
            {
                Praticas = true;
                SetCategorias(Categorias - 1024);
            }
            else if (Categorias >= 512)
            {
                Criatividade = true;
                SetCategorias(Categorias - 512);
            }
            else if (Categorias >= 256)
            {
                VidaSocial = true;
                SetCategorias(Categorias - 256);
            }
            else if (Categorias >= 128)
            {
                RelacionamentoAmoroso = true;
                SetCategorias(Categorias - 128);
            }
            else if (Categorias >= 64)
            {
                RelacionamentoFamilia = true;
                SetCategorias(Categorias - 64);
            }
            else if (Categorias >= 32)
            {
                Contribuicao = true;
                SetCategorias(Categorias - 32);
            }
            else if (Categorias >= 16)
            {
                Recursos = true;
                SetCategorias(Categorias - 16);
            }
            else if (Categorias >= 8)
            {
                Realizacao = true;
                SetCategorias(Categorias - 8);
            }
            else if (Categorias >= 4)
            {
                Equilibrio = true;
                SetCategorias(Categorias - 4);
            }
            else if (Categorias >= 2)
            {
                Desenvolvimento = true;
                SetCategorias(Categorias - 2);
            }
            else if (Categorias >= 1)
            {
                Saude = true;
                SetCategorias(Categorias - 1);
            }
        }
    }
}

