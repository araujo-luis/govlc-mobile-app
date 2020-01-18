using GoVLC.Models;
using Plugin.LocalNotifications;
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
	public partial class VisitFormPage : ContentPage
	{
        //DECLARACIÓN DE VARIABLES NECESARIAS PARA REALIZAR CONEXIÓN CON BASE DE DATOS
        private SQLiteAsyncConnection connection;
        private VLCMonument monument;
        private Visit visit;
        private bool update = false;
		public VisitFormPage ()
		{
			InitializeComponent ();
		}

        public VisitFormPage(VLCMonument monument, Visit visit = null)
        {
            //INSTACIACÓN DE VARIABLES Y VALIDACIÓN PARA VERIFICAR SI SE DEBE DE CREAR NUEVA VISITA O ACTUALIZARLA
            InitializeComponent();
            this.monument = monument;

            if (visit == null)
                update = false;
            else
                update = true;

            this.visit = visit;
            BindingContext = visit;
            
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
        }
        public VisitFormPage(Visit visit)
        {

        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //MÉTODO PARA GUARDAR LA VISITA Y DEFINIR NOTIFICACIONES QUE RECUERDAN UN DÍA ANTERIOR A LA VISITA
            if (visit == null)
                visit = new Visit();
            
            visit.MonumentId = monument.Idnotes;
            visit.VisitDate = new DateTime(datePicker.Date.Year, datePicker.Date.Month,datePicker.Date.Day, timePicker.Time.Hours,timePicker.Time.Minutes, timePicker.Time.Seconds);
            visit.Description = description.Text;
            if (update)
            {
                CrossLocalNotifications.Current.Cancel(visit.Id);
                await connection.UpdateAsync(visit);
            }
            else
            {
                await connection.InsertAsync(visit);
            CrossLocalNotifications.Current.Show("Visitas", "Recuerta que tienes planificado visitar " + monument.Nombre + " mañana a las " + visit.VisitDate.TimeOfDay.ToString(), visit.Id, visit.VisitDate.AddDays(-1));

            }
            await DisplayAlert("Visitas", "Visita agregada", "OK");
            await Navigation.PopAsync();


        }
    }
}