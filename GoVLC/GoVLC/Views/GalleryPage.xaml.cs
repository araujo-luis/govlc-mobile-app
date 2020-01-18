using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoVLC.Models;
using GoVLC.Services;
using Plugin.FacebookClient;
using Plugin.FacebookClient.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GoVLC.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GalleryPage : ContentPage
	{
        private VLCMonument monument;
        //DECLARACÓN DEL SERVICIO
        private VisitService visitService;
        public GalleryPage ()
		{
			InitializeComponent ();
		}

        public GalleryPage(VLCMonument monument)
        {
            //ASIGNACIÓN DE MONUMENTO SELECCIONADO E INSTANSIACIÓN DE SERVICIO
            InitializeComponent();
            this.monument = monument;
            visitService = new VisitService();

        }

        protected async  override void OnAppearing()
        {
            //CARGA DE IMAGES ADJUNTAS A MONUMENTO, LLAMANDO FUNCIÓN DEL SERVICIO
            
            List<AttachedImages> attached = await visitService.GetAttachedImages(monument.Idnotes);
            imagesLista.ItemsSource = attached;
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //METODO UTILIZADO PARA TOMAR LA FOTOGRAFÍA, UTILIZANDO EL PLUGIN DE MEDIA
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                SaveToAlbum = true
            });
            if (photo != null)
            {
                //GUARDAR IMAGEN EN GALERÍA DEL TELÉFONO
                await visitService.SaveAttachedImages(monument.Idnotes, photo.AlbumPath);

                

            }
            else
            {
                Debug.Write("Error");
            }

        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            //METODO QUE COMPARTE FOTOGRAFÍA SELECCIONADA EN FACEBOOK, UTILIZA EL PLUGIN FacebookClient
            var image = (sender as MenuItem).CommandParameter as AttachedImages;
            
            var path = image.Images; ;
            var uri = new Uri(path);
            FacebookSharePhoto photoShare = new FacebookSharePhoto("GOVLC", uri);
            FacebookSharePhoto[] photos = new FacebookSharePhoto[] { photoShare };
            FacebookSharePhotoContent photoContent = new FacebookSharePhotoContent(photos);
            await CrossFacebookClient.Current.ShareAsync(photoContent);

        }
    }
}