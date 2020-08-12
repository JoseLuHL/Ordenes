using SQLite;
using SwipeMenu.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XFFurniture.Models
{
    public class Subcategorium
    {
        [PrimaryKey, AutoIncrement]
        public int SubcatId { get; set; }
        public string SubcatDescripcion { get; set; }
        public string SubcatFoto { get; set; }
        public int? SubcatIdcategoria { get; set; }
        public Boolean Activo { get; set; }
        //public virtual Categoria SubcatIdcategoriaNavigation { get; set; }
        //public virtual ICollection<ProductoModelo> Productos { get; set; }
    }
}
