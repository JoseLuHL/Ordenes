using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XFFurniture.Models
{
   public class OrdenLite
    {

        [PrimaryKey, AutoIncrement]
        public int OrdId { get; set; }
        public string OrdNumero { get; set; }
        public int? OrdIdcliente { get; set; }
        public int? OrdIdtienda { get; set; }
        public string OrdDireccion { get; set; }
        public string OrdAltura { get; set; }
        public string OrdTelefono { get; set; }
        public double? OrdTotalcompra { get; set; }
        public DateTime? OrdFecha { get; set; }
        public double? OrdLatitud { get; set; }
        public double? OrdLongitud { get; set; }
        public DateTime? OrdFechaenvio { get; set; }

        public int? OrdIdestado { get; set; }
        public string OrdDescripcion { get; set; }
        public int? OrdIdformapago { get; set; }
    }
}
