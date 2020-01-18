
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using GoVLC.Models;

//using GoVLC.ViewModels;
using System.Threading.Tasks;
using Plugin.Geolocator;
using System.Diagnostics;

using CoordinateSharp;
using System.Collections.Generic;
using Plugin.Messaging;

namespace GoVLC.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonumentDetailPage : ContentPage
    {
  //      MonumentDetailViewModel viewModel;
        //DECLARACIÓN DE MONUMENTO SELECCIONADO
        VLCMonument monument;
       /*
        public MonumentDetailPage(MonumentDetailViewModel viewModel)
        {
            InitializeComponent();

            //BindingContext = this.viewModel = viewModel;
        }
        */
        public MonumentDetailPage(VLCMonument monument)
        {
            //ASIGNACIÓN DE MONUMENTO SELECCIONADO, VALIDACIÓN SI TIENE TELÉFONO  Y SI TIENE DIRECCIÓN PARA SER MOSTRADA
            InitializeComponent();

            BindingContext  = monument;
            this.monument = monument;
            if(monument.Telefono != "0")
            {
                List<VLCMonument> list = new List<VLCMonument>();
                list.Add(monument);
                listView.IsVisible = true;
                listView.ItemsSource = list;
            }
            if(monument.Address!=null)
            {
                labelAddress.IsVisible = true;
                labelFullAddress.IsVisible = true;
            }

            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //LLAMADA A FUNCIÓN PARA MOSTRAR PIN DEL MONUMENTO EN EL MAPA
            await GetLocation();
            
        }



        private async Task GetLocation()
        {
            Debug.Write("LATITUD Y LONGITUD ");
            Debug.Write(monument.Coordinates[0]);
            Debug.Write(monument.Coordinates[1]);
            //FUNCIÓN QUE DIBUJA PIN DEL MONUMENTO EN EL MAPA, ADEMÁS DE LA CONVERSIÓN DE COORDENADAS A LATITUD Y LONGITUD
            var locator = CrossGeolocator.Current;
            var location = await locator.GetLastKnownLocationAsync();
            
            UniversalTransverseMercator utm = new UniversalTransverseMercator("S", 30, monument.Coordinates[0], monument.Coordinates[1]);
            Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);
            

            Position position = new Position(c.Latitude.DecimalDegree, c.Longitude.DecimalDegree);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Xamarin.Forms.Maps.Distance.FromMiles(.5)));

            //DIBUJO DE PIN EN MAPA
            map.Pins.Add(new Pin()
            {
                Position = new Position(c.Latitude.DecimalDegree, c.Longitude.DecimalDegree),
                Label = monument.Nombre
            });
            

        }
        /*
        public MonumentDetailPage()
        {
            InitializeComponent();

            var item = new Monument
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new MonumentDetailViewModel(item);
            BindingContext = viewModel;
        }
        */
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //MÉTODO PARA REALIZAR LLAMADA TELEFÓNICA UTILIZANDO EL PLUGIN CrossMessaging
            if (e.SelectedItem == null)
                return;
            var item = e.SelectedItem as VLCMonument;
            Debug.Write(item.Telefono);
            var phoneCall = CrossMessaging.Current.PhoneDialer;
            
            if (phoneCall.CanMakePhoneCall)
            {
                phoneCall.MakePhoneCall("+34" + item.Telefono, item.Nombre);
            }
            listView.SelectedItem = null;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            //LLAMADA A PÁGINA PARA REGISTRAR UNA VISITA PLANEADA
            await Navigation.PushAsync(new VisitsPage(monument));
        }
        /*
        private async void CamaraButton_Clicked(object sender, System.EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                SaveToAlbum = true
            });
            if (photo != null)
            {
                
                PhotoImage.Source = ImageSource.FromStream(() =>
                {
                    return photo.GetStream();
                });
                CamaraButton.Text = photo.AlbumPath;
            }
            var path = photo.AlbumPath;


        }
        */
        private async void ToolbarItem_Clicked_1(object sender, System.EventArgs e)
        {
            //LLAMADA A PÁGINA PARA VER GALERÍA DE IMÁGENES
            await Navigation.PushAsync(new GalleryPage(monument));
        }
    }
}