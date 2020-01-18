using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public class Visit
    {
        //CLASE QUE MAPEA LAS VISITAS A LOS MONUMENTOS
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string MonumentId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; }

    }
}
