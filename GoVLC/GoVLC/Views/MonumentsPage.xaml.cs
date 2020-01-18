using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GoVLC.Models;
using GoVLC.Views;
//using GoVLC.ViewModels;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

using System.Reflection;
using System.IO;
using GoVLC.Services;

namespace GoVLC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonumentsPage : ContentPage
    {
        
         //DEFINICIÓN DE SERVICIO
        
        private MonumentService monumentService;
        

        //MonumentsViewModel viewModel;

        public MonumentsPage()
        {
            //INSTANCIACIÓN DE SERVICIO
            InitializeComponent();
            monumentService = new MonumentService();
            //BindingContext = viewModel = new MonumentsViewModel();
        }
        protected override  void OnAppearing()
        {
            //LLAMADA A MÉTODO DEL SERIVIO PARA OBTENER TODOS LOS MONUMENTOS
            monumentsList.ItemsSource = monumentService.GetLCMonuments();

                
           
            
            //_monuments = new ObservableCollection<VLCMonument>(monumentsJSON);
            //monumentsList.ItemsSource = _monuments;
            base.OnAppearing();

        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //MÉTODO QUE OBTIENE DIRECCIÓN E IMAGE DEL MONUMENTO QUE SERÁ MOSTRADO EN LA PAGINA DE DETALLE
            var item = args.SelectedItem as VLCMonument;
            if (item == null)
                return;
            Address a = monumentService.GetAddress(item.Codvia).FirstOrDefault();
            MonumentImage mi = await monumentService.GetImages(item.Idnotes);
            item.Address = a;
            item.Image = mi;
            await Navigation.PushAsync(new MonumentDetailPage(item));

            
            monumentsList.SelectedItem = null;
        }

       /* async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }
        */
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MÉTODO QUE REALIZA BÚSQUEDA DE MONUMENTOS POR NOMNRE
            monumentsList.ItemsSource = monumentService.GetLCMonuments(e.NewTextValue.ToUpper());
        }
    }
}