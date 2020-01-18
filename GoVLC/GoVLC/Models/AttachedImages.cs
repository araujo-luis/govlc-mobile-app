using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public class AttachedImages
    {
        //CLASE PARA MAPEAR IMAGENES ADJUNTAS A  UN MONUMENTO
        public string Id { get; set; }
        public string Images { get; set; }

        public AttachedImages(string id, string image)
        {
            Id = id;
            Images = image;
        }
        public AttachedImages()
        {

        }

    }
}
