using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace GoVLC.Models
{
    public class CustomMap : Map
    {
        //CLASE PARA PERSONALIZAR EL MAPA
        public List<CustomPin> CustomPins { get; set; }
    }
}
