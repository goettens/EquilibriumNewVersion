using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EquilibriumApp.ViewModels
{
    public class AdicionarViewModel : BaseViewModel
    {
        public AdicionarViewModel()
        {
            Title = "Adicionar uma Atividade";
            SelecionarCommand = new Command(() => SelectImage());
        }

        public ICommand SelecionarCommand { get; set; }

        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set { SetProperty(ref image, value, "Image"); }
        }

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
                        PhotoSize = PhotoSize.Medium
                    };

                    // Take a photo of the business receipt.
                    var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    Image = ImageSource.FromStream(() => selectedImageFile.GetStream());
                
                }
            }
            catch (Exception)
            {
                await DialogService.ShowMessage("É necessário permitir a aplicação para selecionar fotos", "Acesso Negado");
                throw;
            }
        }
    }
}
