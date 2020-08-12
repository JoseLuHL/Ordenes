using SQLite;
using SwipeMenu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using XFFurniture.ViewModel;

namespace XFFurniture.Models
{
    public class Categoria
    {
        [PrimaryKey, AutoIncrement]
        public int CantId { get; set; }
        public string CatDescripcion { get; set; }
        public string CatIdestado { get; set; }
        public string CatFoto { get; set; }
        public bool Todos { get; set; }
        //public bool Todos { get; set; }
        //private bool todos;
        //[Ignore]
        //public bool Todos
        //{
        //    get => todos;
        //    set => SetProperty(ref todos, value);
        //}
        //[Ignore]
        //public virtual ICollection<Detalletiendacategorium> Detalletiendacategoria { get; set; }
        //[Ignore]
        //public virtual ICollection<Subcategorium> Subcategoria { get; set; }
    }
}