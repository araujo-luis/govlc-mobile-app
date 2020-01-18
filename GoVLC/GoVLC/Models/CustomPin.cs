using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace GoVLC.Models
{
    public class CustomPin : Pin
    {
        //CLASE PARA PERSONALIZAR PIN DEL MAPA
        public string Url { get; set; }
        public string Icon { get; set; }
    }
}
