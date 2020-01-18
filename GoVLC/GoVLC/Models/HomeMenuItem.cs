using System;
using System.Collections.Generic;
using System.Text;

namespace GoVLC.Models
{
    public enum MenuItemType
    {
        Monuments,
        MonumentsMap,
        About
    }
    public class HomeMenuItem
    {
        //CLASE PARA DEFINIR LAS PÁGINAS QUE ESTARÁN EN EL MENU PRINCIPAL
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
