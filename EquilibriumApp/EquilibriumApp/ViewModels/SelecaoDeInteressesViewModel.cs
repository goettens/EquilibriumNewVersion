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
            set
            {
                SetProperty(ref numCat, value, nameof(NumCat));
                string a = numCat.ToString();
            }
        }

        #region CATEGORIAS BOOLEAN
        private bool saude = false;
        public bool Saude
        {
            get { return saude; }
            set{
                if ((saude && value) || (!saude && !value))
                    return;
                SetProperty(ref saude, value, nameof(Saude));
                NumCat = value ? numCat + 1 : numCat - 1;
            }
        }

        private bool desenvolvimento = false;
        public bool Desenvolvimento
        {
            get { return desenvolvimento; }
            set {
                if ((desenvolvimento && value) || (!desenvolvimento && !value))
                    return;
                SetProperty(ref desenvolvimento, value, nameof(Desenvolvimento));
                NumCat = value ? numCat + 2 : numCat - 2;
            }
        }

        private bool equilibrio = false;
        public bool Equilibrio
        {
            get { return equilibrio; }
            set {
                if ((equilibrio && value) || (!equilibrio && !value))
                    return;
                SetProperty(ref equilibrio, value, nameof(Equilibrio));
                NumCat = value ? numCat + 4 : numCat - 4;
            }
        }

        private bool realizacao = false;
        public bool Realizacao
        {
            get { return realizacao; }
            set {
                if ((realizacao && value) || (!realizacao && !value))
                    return;
                SetProperty(ref realizacao, value, nameof(Realizacao));
                NumCat = value ? numCat + 8 : numCat - 8;
            }
        }

        private bool recursos = false;
        public bool Recursos
        {
            get { return recursos; }
            set {
                if ((recursos && value) || (!recursos && !value))
                    return;
                SetProperty(ref recursos, value, nameof(Recursos));
                NumCat = value ? numCat + 16 : numCat - 16;
            }
        }

        private bool contribuicao = false;
        public bool Contribuicao
        {
            get { return contribuicao; }
            set {
                if ((contribuicao && value) || (!contribuicao && !value))
                    return;
                SetProperty(ref contribuicao, value, nameof(Contribuicao));
                NumCat = value ? numCat + 32 : numCat - 32;
            }
        }

        private bool relacionamentoFamila = false;
        public bool RelacionamentoFamilia
        {
            get { return relacionamentoFamila; }
            set {
                if ((relacionamentoFamila && value) || (!relacionamentoFamila && !value))
                    return;
                SetProperty(ref relacionamentoFamila, value, nameof(RelacionamentoFamilia));
                NumCat = value ? numCat + 64 : numCat - 64;
            }
        }

        private bool relacionamentoAmoroso = false;
        public bool RelacionamentoAmoroso
        {
            get { return relacionamentoAmoroso; }
            set {
                if ((relacionamentoAmoroso && value) || (!relacionamentoAmoroso && !value))
                    return;
                SetProperty(ref relacionamentoAmoroso, value, nameof(RelacionamentoAmoroso));
                NumCat = value ? numCat + 128 : numCat - 128;
            }
        }

        private bool vidaSocial = false;
        public bool VidaSocial
        {
            get { return vidaSocial; }
            set {
                if ((vidaSocial && value) || (!vidaSocial && !value))
                    return;
                SetProperty(ref vidaSocial, value, nameof(VidaSocial));
                NumCat = value ? numCat + 256 : numCat - 256;
            }
        }

        private bool criatividade = false;
        public bool Criatividade
        {
            get { return criatividade; }

            set {
                if ((criatividade && value) || (!criatividade && !value))
                    return;
                SetProperty(ref criatividade, value, nameof(Criatividade));
                NumCat = value ? numCat + 512 : numCat - 512;
            }
        }

        private bool praticas = false;
        public bool Praticas
        {
            get { return praticas; }
            set {
                if ((praticas && value) || (!praticas && !value))
                    return;
                SetProperty(ref praticas, value, nameof(Praticas));
                NumCat = value ? NumCat + 1024 : NumCat - 1024;
            }
        }

        private bool plenitude = false;
        public bool Plenitude
        {
            get { return plenitude; }
            set {
                if ((plenitude && value) || (!plenitude && !value))
                    return;

                SetProperty(ref plenitude, value, nameof(Plenitude));
                NumCat = value ? numCat + 2048 : numCat - 2048;
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

                SettingsService.EnumCategorias = Usuario.EnumCategories;
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
            SettingsService.EnumCategorias = NumCat;
            try
            {

            await NavigationService.NavigateToAsync<FeedViewModel>(NumCat);
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

