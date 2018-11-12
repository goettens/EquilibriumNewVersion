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
        private int NumCat
        {
            get { return numCat; }
            set { SetProperty(ref numCat, value, nameof(NumCat));
                string a = numCat.ToString();
            }
        }

        #region CATEGORIAS BOOLEAN
        private bool saude;
        public bool Saude
        {
            get { return saude; }
            set {
                saude = value;
                NumCat = value ? numCat + 1 : numCat - 1;
                OnPropertyChanged(nameof(Saude));
            }
        }

        private bool desenvolvimento;
        public bool Desenvolvimento
        {
            get { return desenvolvimento; }
            set {
                desenvolvimento = value;
                NumCat = value ? numCat + 2 : numCat - 2;
                OnPropertyChanged(nameof(Desenvolvimento));
            }
        }

        private bool equilibrio;
        public bool Equilibrio
        {
            get { return equilibrio; }
            set {
                equilibrio = value;
                NumCat = value ? numCat + 4 : numCat - 4;
                OnPropertyChanged(nameof(Equilibrio));
            }
        }

        private bool realizacao;
        public bool Realizacao
        {
            get { return realizacao; }
            set {
                realizacao = value;
                NumCat = value ? numCat + 8 : numCat - 8;
                OnPropertyChanged(nameof(Realizacao));
            }
        }

        private bool recursos;
        public bool Recursos
        {
            get { return recursos; }
            set {
                recursos = value;
                NumCat = value ? numCat + 16 : numCat - 16;
                OnPropertyChanged(nameof(Recursos));
            }
        }

        private bool contribuicao;
        public bool Contribuicao
        {
            get { return contribuicao; }
            set {
                contribuicao = value;
                NumCat = value ? numCat + 32 : numCat - 32;
                OnPropertyChanged(nameof(Contribuicao));
            }
        }

        private bool relacionamentoFamila;
        public bool RelacionamentoFamilia
        {
            get { return relacionamentoFamila; }
            set {
                relacionamentoFamila = value;
                NumCat = value ? numCat + 64 : numCat - 64;
                OnPropertyChanged(nameof(RelacionamentoFamilia));
            }
        }

        private bool relacionamentoAmoroso;
        public bool RelacionamentoAmoroso
        {
            get { return relacionamentoAmoroso; }
            set {
                relacionamentoAmoroso = value;
                NumCat = value ? numCat + 128 : numCat - 128;
                OnPropertyChanged(nameof(RelacionamentoAmoroso));
            }
        }

        private bool vidaSocial;
        public bool VidaSocial
        {
            get { return vidaSocial; }
            set {
                vidaSocial = value;
                NumCat = value ? numCat + 256 : numCat - 256;
                OnPropertyChanged(nameof(VidaSocial));
            }
        }

        private bool criatividade;
        public bool Criatividade
        {
            get { return criatividade; }
            set {
                criatividade = value;
                NumCat = value ? numCat + 512 : numCat - 512;
                OnPropertyChanged(nameof(Criatividade));
            }
        }

        private bool praticas;
        public bool Praticas
        {
            get { return praticas; }
            set {
                praticas = value;
                NumCat = value ? NumCat + 1024 : NumCat - 1024;
                OnPropertyChanged(nameof(Praticas));
            }
        }

        private bool plenitude;
        public bool Plenitude
        {
            get { return plenitude; }
            set { plenitude = value;
                NumCat = value ? numCat + 2048 : numCat - 2048;
                OnPropertyChanged(nameof(Plenitude));
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

                NumCat = 0;
                SetCategorias(Usuario.EnumCategories);

            }
            catch(Exception ex)
            {
               await DialogService.ShowMessage(ex.ToString(), "error");
            }
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
                return;
            }
            else if (Categorias >= 1024)
            {
                Praticas = true;
                SetCategorias(Categorias - 1024);
                return;
            }
            else if (Categorias >= 512)
            {
                Criatividade = true;
                SetCategorias(Categorias - 512);
                return;
            }
            else if (Categorias >= 256)
            {
                VidaSocial = true;
                SetCategorias(Categorias - 256);
                return;
            }
            else if (Categorias >= 128)
            {
                RelacionamentoAmoroso = true;
                SetCategorias(Categorias - 128);
                return;
            }
            else if (Categorias >= 64)
            {
                RelacionamentoFamilia = true;
                SetCategorias(Categorias - 64);
                return;
            }
            else if (Categorias >= 32)
            {
                Contribuicao = true;
                SetCategorias(Categorias - 32);
                return;
            }
            else if (Categorias >= 16)
            {
                Recursos = true;
                SetCategorias(Categorias - 16);
                return;
            }
            else if (Categorias >= 8)
            {
                Realizacao = true;
                SetCategorias(Categorias - 8);
                return;
            }
            else if (Categorias >= 4)
            {
                Equilibrio = true;
                SetCategorias(Categorias - 4);
                return;
            }
            else if (Categorias >= 2)
            {
                Desenvolvimento = true;
                SetCategorias(Categorias - 2);
                return;
            }
            else if (Categorias >= 1)
            {
                Saude = true;
                SetCategorias(Categorias - 1);
                return;
            }
        }
    }
}

