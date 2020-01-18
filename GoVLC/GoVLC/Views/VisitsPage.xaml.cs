using GoVLC.Models;
using GoVLC.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoVLC.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisitsPage : ContentPage
	{
        //DECLARACIÓN DE VARIABLES DEL MONUMENTO Y SERVICIO
        private VLCMonument monument;
        
        private VisitService visitService;

		public VisitsPage ()
		{
			InitializeComponent ();
		}

        public VisitsPage(VLCMonument monument)
        {
            //INSTANCIACIÓN DE VARIABLES DECLARADAS
            InitializeComponent();
            this.monument = monument;
            visitService = new VisitService();
            

        }

        protected override async void OnAppearing()
        {
            //MÉTODO QUE OBTIENE LAS VISITAS REGISTRADAS EN EL MONUMENTO ESPECÍFICO
            List<Visit> visits = await visitService.GetVisits(monument.Idnotes);
            
            NoVisits.IsVisible = (visits.Count == 0) ? true : false;

            visitsList.ItemsSource = visits;
            base.OnAppearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //MÉTODO PARA ABRIR FORMULARIO DE NUEVA VISITA
            await Navigation.PushAsync(new VisitFormPage(monument));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            //MÉTODO PARA ELIMINAR UNA VISITA
            var visit = (sender as MenuItem).CommandParameter as Visit;
            await visitService.deleteVisit(visit);
            await DisplayAlert("Visitas", "Visita eliminada", "OK");
            OnAppearing();
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            //MÉTODO PARA ACTUALIZAR UNA VISITA
            var visit = (sender as MenuItem).CommandParameter as Visit;
            Navigation.PushAsync(new VisitFormPage(monument, visit));
        }
    }
}