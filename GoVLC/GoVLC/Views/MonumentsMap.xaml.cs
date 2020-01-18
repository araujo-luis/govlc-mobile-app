using CoordinateSharp;
using GoVLC.Models;
using GoVLC.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace GoVLC.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonumentsMap : ContentPage
	{
        //INSTANCIACIÓN DE SERVICIO
        private MonumentService monumentService = new MonumentService();
		public MonumentsMap ()
		{
			InitializeComponent ();

		}
        protected override async void OnAppearing()
        {
            //MÉTODO PARA OBTENER LA LOCATION ACTUAL Y TODOS LOS MONUMENTOS QUE SERÁN DIBUJADOS EN EL MAPA
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var location = await locator.GetLastKnownLocationAsync();
            IEnumerable<VLCMonument>  monuments =  monumentService.GetLCMonuments();

            drawPins(monuments);
           
        }

        private void drawPins(IEnumerable<VLCMonument> monuments)
        {
            //MÉTODP QUE RECIBE COMO PARÁMETRO LOS MONUMENTOS QUE SERÁN DIBUJADOS EN EL MAPA
            List<CustomPin> listPin = new List<CustomPin>();
            //RECORRIDO DE LISTADO Y POR CADA UNO CONVIERTE LAS COORDENADAS Y CREA UN PIN QUE COLOCARÁ EN EL MAPA
            foreach (var monument in monuments)
            {
                UniversalTransverseMercator utm = new UniversalTransverseMercator("S", 30, monument.Coordinates[0], monument.Coordinates[1]);
                Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

                Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position(c.Latitude.DecimalDegree, c.Longitude.DecimalDegree);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Xamarin.Forms.Maps.Distance.FromMiles(.5)));

                var pin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Xamarin.Forms.Maps.Position(c.Latitude.DecimalDegree, c.Longitude.DecimalDegree),
                    Label = monument.Nombre,
                    Icon = monument.Imagen.Substring(0, monument.Imagen.Length - 4),

                };
                listPin.Add(pin);

                map.Pins.Add(pin);


            }
            map.CustomPins = listPin;



        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MÉTODO QUE OBTIENE FILTRO INGRESADO POR EL USUARIO Y A PARTIR DE ESE DATO, LLAMA AL SERVICIO QUE DEVUELVE LOS MONUMENTOS CORRESPONDIENTES
            var selectedFilter = filter.Items[filter.SelectedIndex];
            selectedFilter = selectedFilter == "Todos" ? null : selectedFilter;
            map.Pins.Clear();

            drawPins(monumentService.GetVLCMonumentsFiltered(selectedFilter));

        }

        
    }
}