using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public interface ISQLiteDB
    {
        //INTERFAZ PARA HACER LA CONEXIÓN CON SQL
        SQLiteAsyncConnection GetConnection();
    }
}
