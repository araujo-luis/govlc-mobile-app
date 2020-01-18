
using CsvHelper;
using GoVLC.Models;
using GoVLC.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoVLC.Services
{
    public class MonumentService
    {
        //SERVICIO PARA OBTENER DATA DE LOS MONUMENTOS
        private HttpClient client = new HttpClient();
        private const string url = "https://govlc.herokuapp.com/";
        public MonumentService()
        {

        }

       public IEnumerable<VLCMonument> GetLCMonuments(string searchText = null)
        {
            //MÉTODO QUE OBTIENE TODOS LOS MONUMENTOS PARA MOSTRARLOS EN LA LISTA
            var assembly = typeof(MonumentsPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GoVLC.data.json");
            string text = "";

            using (var reader = new System.IO.StreamReader(stream))
            {

                text = reader.ReadToEnd();
            }
            var monumentsList = JsonConvert.DeserializeObject<List<VLCMonument>>(text);
            if (String.IsNullOrWhiteSpace(searchText))  
                return monumentsList;
            

            return monumentsList.Where(x => x.Nombre.StartsWith(searchText));


        }

        

        public IEnumerable<VLCMonument> GetVLCMonumentsFiltered(string filter)
        {
            //MÉTODO QUE OBTIENE LOS MONUMENTOS BUSCADOS POR NOMBRE
            var assembly = typeof(MonumentsPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GoVLC.data.json");
            string text = "";

            using (var reader = new System.IO.StreamReader(stream))
            {

                text = reader.ReadToEnd();
            }
            var monumentsList = JsonConvert.DeserializeObject<List<VLCMonument>>(text);
            if (String.IsNullOrWhiteSpace(filter))
                return monumentsList;


            return monumentsList.Where(x => x.Categoria.StartsWith(filter));


        }

        public IEnumerable<Address> GetAddress(string viaid)
        {
            //MÉTODO QUE OBTIENE LA DIRECCIÓN DE UN MONUMENTO ESPECÍFICO DESDE UN ARCHIVO CSV
            var assembly = typeof(MonumentsPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GoVLC.vias.csv");
            string text = "";

            

            using (var reader = new System.IO.StreamReader(stream))
                using (var csv = new CsvReader(reader))
            {
                text = reader.ReadToEnd();
                
                
                
            }
            var textReader = new StringReader(text);
            var csvr = new CsvReader(textReader);
            csvr.Configuration.RegisterClassMap<AddressMap>();
            
            var records = csvr.GetRecords<Address>().ToList();
            var list = records.ToList();
            var thisone = records.Find(x => x.Codvia.Equals(Convert.ToInt32(viaid)));
            return records.Where(x => x.Codvia.Equals(Convert.ToInt32(viaid)));

            //           var addressList = JsonConvert.DeserializeObject<List<Address>>(text);

        }

        public async Task<MonumentImage> GetImages(string id)
        {
            //PETICIÓN A API PARA OBTENER LAS IMÁGENES DE LOS MONUMENTOS
            var images = await client.GetStringAsync(url+"messages/"+id);
            var result = JsonConvert.DeserializeObject<MonumentImage>(images);
            return result;

        }

        

    }
}
