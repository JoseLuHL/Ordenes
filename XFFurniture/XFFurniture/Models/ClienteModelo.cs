using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeMenu.Models
{
   public class ClienteModelo
    {

        public int ClieId { get; set; }
        public string ClieIdtipoidentificacion { get; set; }
        public string ClieIdentificacion { get; set; }
        public string ClieNombre { get; set; }
        public string ClieApellidos { get; set; }
        

        public string ClieDireccion { get; set; }
        public string ClieIdsexo { get; set; }
        public string ClieTelefono { get; set; }
        public string ClieBarrio { get; set; }
        public string ClieLongitud { get; set; }
        public string ClieLatitud { get; set; }
        public string ClieAltura { get; set; }
        public virtual ICollection<OrdenModelo> Ordenes { get; set; }

        private string nombreCompleto;

        public string NombreCompleto
        {
            get { return $"{ClieNombre} {ClieApellidos}"; }
            set { nombreCompleto = value; }
        }
    }
}
