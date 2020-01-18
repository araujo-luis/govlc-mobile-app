using GoVLC.Models;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoVLC.Services
{
    public class VisitService
    {
        //SERVICIO QUE MANEJA DATA DE LAS VISITAS
        private SQLiteAsyncConnection connection;
        public VisitService()
        {
            connection = DependencyService.Get<ISQLiteDB>().GetConnection();
        }

        public async Task<List<Visit>> GetVisits(string id)
        {
            //MÉTODO QUE OBTIENE LAS VISITAS DE UN MONUMENTOS ESPECÍFICO
            await connection.CreateTableAsync<Visit>();
            
            var visits = await connection.Table<Visit>().Where(x => id.Equals(x.MonumentId)).ToListAsync();
            return visits;
        }

        public async Task<int> deleteVisit(Visit visit)
        {
            //MÉTODO QUE ELIMINA UNA VISITA
            return await connection.DeleteAsync(visit);
        }
        public async Task<List<AttachedImages>> GetAttachedImages(string idnotes)
        {
            //MÉTODO QUE OBTIENE LAS IMÁGENES ADJUTAS POR EL USUARIO
            //await connection.ExecuteAsync("drop table AttachedImages");
            await connection.CreateTableAsync<AttachedImages>();
            var visits = await connection.Table<AttachedImages>().Where(x => idnotes.Equals(x.Id)).ToListAsync();
            return visits;
        }

        public async Task SaveAttachedImages(string idnotes, string albumPath)
        {
            //MÉTODO PARA ALMACENAR LAS IMÁGENES ADJUNTAS POR EL USUARIO
            await connection.CreateTableAsync<AttachedImages>();
            var attached = new AttachedImages(idnotes, albumPath);
            
            await connection.InsertOrReplaceAsync(attached);
            
            
        }
    }
}
