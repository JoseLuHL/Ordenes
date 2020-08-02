using SwipeMenu.Models;
using System;
using System.Collections.Generic;


namespace QP_Comercio_Electronico.Models
{
    public partial class Estadoorden
    {
        

        public int EsorId { get; set; }
        public string EsorIdDescripcion { get; set; }

        //public virtual ICollection<Ordene> Ordenes { get; set; }
        private ICollection<OrdenModelo> ordenes { get; set; }
        public virtual ICollection<OrdenModelo> Ordenes
        {
            get { return null; }
            set { ordenes = value; }
        }
    }
}
