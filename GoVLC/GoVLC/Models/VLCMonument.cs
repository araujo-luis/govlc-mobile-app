using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public class VLCMonument
    {
        //CLASE QUE MAPEA LOS ATRIBUTOS DE LOS MONUMENTOS
        
        public string Nombre { get; set; }
        public string Numpol { get; set; }
        public string Idnotes { get; set; }
        public string Codvia { get; set; }
        public string Telefono { get; set; }
        public string Ruta { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
        public double[] Coordinates { get; set; }
        public Address Address { get; set; }
        public MonumentImage Image { get; set; }
        public List<String> AttachedPictures { get; set; }
    }
}
