using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            //CLASE PARA MAPEAR ATRIBUTOS DE LOS MONUMENTOS
            Map(m => m.Codtipovia).Name("codtipovia");
            Map(m => m.Codvia).Name("codvia");
            Map(m => m.Codviacatastro).Name("codviacatastro");
            Map(m => m.Nomoficial).Name("nomoficial");
            Map(m => m.Traducnooficial).Name("traducnooficial");
        }
    }
}
