using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public class MonumentImage
    {
        //CLASE PARA MAPEAR IMAGENES DEL MONUMENTO QUE SE OBTIENEN POR LA API
        public int Id { get; set; }
        public List<String> Images { get; set; }
    }
}
