using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeMenu.Models
{
   public class Ordendetalle
    {
        public int DetordId { get; set; }
        public int? DetordIdproducto { get; set; }
        public int? DetordOrdennumero { get; set; }
        public string DetordPrecio { get; set; }
        public string DetordCantidad { get; set; }
        public string DetordDescuento { get; set; }
        public string DetordTotal { get; set; }
        public string DetordTamano { get; set; }
        public string DetordColor { get; set; }
        public string DetordFechaenvio { get; set; }
        public virtual ProductoModelo DetordIdproductoNavigation { get; set; }
        public virtual OrdenModelo DetordOrdennumeroNavigation { get; set; }
    }
}
