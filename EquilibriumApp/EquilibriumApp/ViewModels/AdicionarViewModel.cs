using EquilibriumApp.Models;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Database.Query;

namespace EquilibriumApp.ViewModels
{
    public class AdicionarViewModel : BaseViewModel
    {
        public AdicionarViewModel()
        {
            Title = "Adicionar uma Atividade";
            SelecionarCommand = new Command(() => SelectImage());
            PublicarCommand = new Command(async () => await Publicar());
            Publicacao = new Atividade() {
                EnumCategories = 0,
                Id = "0",
                Likes = 0,
                MaximumPrice = 0,
                MinimumPrice = 0,
                ReportsCount = 0,
            };
        }

        public ICommand SelecionarCommand { get; set; }
        public ICommand PublicarCommand { get; set; }


        public Atividade Publicacao { get; set; }

        public MediaFile file;
        private string imagePath;

        public Stream imageStream;
        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set { SetProperty(ref image, value, "Image"); }
        }

        #region CATEGORIAS BOOLEAN
        private bool saude = false;
        public bool Saude
        {
            get { return saude; }
            set
            {
                if ((saude && value) || (!saude && !value))
                    return;
                SetProperty(ref saude, value, nameof(Saude));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 1 : Publicacao.EnumCategories - 1;
            }
        }

        private bool desenvolvimento = false;
        public bool Desenvolvimento
        {
            get { return desenvolvimento; }
            set
            {
                if ((desenvolvimento && value) || (!desenvolvimento && !value))
                    return;
                SetProperty(ref desenvolvimento, value, nameof(Desenvolvimento));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 2 : Publicacao.EnumCategories - 2;
            }
        }

        private bool equilibrio = false;
        public bool Equilibrio
        {
            get { return equilibrio; }
            set
            {
                if ((equilibrio && value) || (!equilibrio && !value))
                    return;
                SetProperty(ref equilibrio, value, nameof(Equilibrio));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 4 : Publicacao.EnumCategories - 4;
            }
        }

        private bool realizacao = false;
        public bool Realizacao
        {
            get { return realizacao; }
            set
            {
                if ((realizacao && value) || (!realizacao && !value))
                    return;
                SetProperty(ref realizacao, value, nameof(Realizacao));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 8 : Publicacao.EnumCategories - 8;
            }
        }

        private bool recursos = false;
        public bool Recursos
        {
            get { return recursos; }
            set
            {
                if ((recursos && value) || (!recursos && !value))
                    return;
                SetProperty(ref recursos, value, nameof(Recursos));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 16 : Publicacao.EnumCategories - 16;
            }
        }

        private bool contribuicao = false;
        public bool Contribuicao
        {
            get { return contribuicao; }
            set
            {
                if ((contribuicao && value) || (!contribuicao && !value))
                    return;
                SetProperty(ref contribuicao, value, nameof(Contribuicao));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 32 : Publicacao.EnumCategories - 32;
            }
        }

        private bool relacionamentoFamila = false;
        public bool RelacionamentoFamilia
        {
            get { return relacionamentoFamila; }
            set
            {
                if ((relacionamentoFamila && value) || (!relacionamentoFamila && !value))
                    return;
                SetProperty(ref relacionamentoFamila, value, nameof(RelacionamentoFamilia));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 64 : Publicacao.EnumCategories - 64;
            }
        }

        private bool relacionamentoAmoroso = false;
        public bool RelacionamentoAmoroso
        {
            get { return relacionamentoAmoroso; }
            set
            {
                if ((relacionamentoAmoroso && value) || (!relacionamentoAmoroso && !value))
                    return;
                SetProperty(ref relacionamentoAmoroso, value, nameof(RelacionamentoAmoroso));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 128 : Publicacao.EnumCategories - 128;
            }
        }

        private bool vidaSocial = false;
        public bool VidaSocial
        {
            get { return vidaSocial; }
            set
            {
                if ((vidaSocial && value) || (!vidaSocial && !value))
                    return;
                SetProperty(ref vidaSocial, value, nameof(VidaSocial));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 256 : Publicacao.EnumCategories - 256;
            }
        }

        private bool criatividade = false;
        public bool Criatividade
        {
            get { return criatividade; }

            set
            {
                if ((criatividade && value) || (!criatividade && !value))
                    return;
                SetProperty(ref criatividade, value, nameof(Criatividade));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 512 : Publicacao.EnumCategories - 512;
            }
        }

        private bool praticas = false;
        public bool Praticas
        {
            get { return praticas; }
            set
            {
                if ((praticas && value) || (!praticas && !value))
                    return;
                SetProperty(ref praticas, value, nameof(Praticas));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 1024 : Publicacao.EnumCategories - 1024;
            }
        }

        private bool plenitude = false;
        public bool Plenitude
        {
            get { return plenitude; }
            set
            {
                if ((plenitude && value) || (!plenitude && !value))
                    return;

                SetProperty(ref plenitude, value, nameof(Plenitude));
                Publicacao.EnumCategories = value ? Publicacao.EnumCategories + 2048 : Publicacao.EnumCategories - 2048;
            }
        }

        #endregion



        public async void SelectImage()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    // Supply media options for saving our photo after it's taken.
                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium,
                        SaveMetaData = true
                    };

                    // Take a photo of the business receipt.
                    file = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    Image = ImageSource.FromStream(() =>
                    {
                        var imageStream = file.GetStream();
                        return imageStream;
                    });

                    imageStream = file.GetStream();
                    imagePath = file.Path;
                }
            }
            catch (Exception)
            {
                await DialogService.ShowMessage("É necessário permitir a aplicação para selecionar fotos", "Acesso Negado");
            }
        }

        public async Task Publicar()
        {
            if(string.IsNullOrEmpty(Publicacao.Name) 
            || string.IsNullOrEmpty(Publicacao.LongDescription)
            || Image == null)
            {
                await DialogService.ShowMessage("Verifique se o nome, a descrição e a imagem estão selecionados", "Campos obrigatórios");
                return;
            }

            try
            {
                var result = await Firebase.Child("recommendedactivities").PostAsync(Publicacao);

                Publicacao.Id = result.Key;

                Publicacao.ImageURL = await StoreImages();

                await Firebase.Child("recommendedactivities").Child(Publicacao.Id).PatchAsync(Publicacao);

                
            }
            catch (Exception ex)
            {
                await DialogService.ShowMessage(ex.ToString(), "Error");
            }

            Publicacao = new Atividade()
            {
                EnumCategories = 0,
                Id = "0",
                Likes = 0,
                MaximumPrice = 0,
                MinimumPrice = 0,
                ReportsCount = 0,
            };

            OnPropertyChanged(nameof(Publicacao));
            Image = null;
            file = null;
            imagePath = null;
            imageStream = null; ;

    }

        public async Task<string> StoreImages()
        {
            string imgurl = null;
            try
            {
                var storageImage = await new FirebaseStorage("roda-da-vida-app.appspot.com", new FirebaseStorageOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(SettingsService.AccessToken),
                    ThrowOnCancel = true
                })
                .Child("images")
                .Child("recommendedactivities")
                .Child(Publicacao.Id + ".jpeg")
                .PutAsync(imageStream);
                imgurl = storageImage;

                await DialogService.ShowMessage(imgurl, "Sucesso");
                
            }
            catch (Exception Ex)
            {
                await DialogService.ShowMessage(Ex.ToString(), "error");
            }
            return imgurl;
        }
    }
}
