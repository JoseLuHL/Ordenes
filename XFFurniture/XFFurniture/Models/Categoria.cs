using SwipeMenu.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XFFurniture.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<ProductoModelo>();
        }

        public int CantId { get; set; }
        public string CatDescripcion { get; set; }
        public string CatIdestado { get; set; }
        public string CatFoto { get; set; }
        public virtual ICollection<ProductoModelo> Productos { get; set; }
    }
}