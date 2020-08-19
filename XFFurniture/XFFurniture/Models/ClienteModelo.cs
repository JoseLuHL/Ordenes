using MvvmHelpers;
using QP_Comercio_Electronico.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


namespace SwipeMenu.Models
{
    public class ClienteModelo
    {
        [PrimaryKey, AutoIncrement]
        public  int id { get; set; }
        public int ClieId { get; set; }
        public string ClieIdtipoidentificacion { get; set; }
        public string ClieIdentificacion { get; set; }
        public string ClieNombre { get; set; }
        public string ClieApellidos { get; set; }
        public string ClieClave { get; set; }
        public string ClieDireccion { get; set; }
        public string ClieIdsexo { get; set; }
        public string ClieTelefono { get; set; }
        public string ClieBarrio { get; set; }
        public string ClieLongitud { get; set; }
        public string ClieLatitud { get; set; }
        public string ClieAltura { get; set; }
        //public virtual ICollection<OrdenModelo> Ordenes { get; set; }
        private string nombreCompleto;
        public virtual string NombreCompleto
        {
            get { return $"{ClieNombre} {ClieApellidos}"; }
            set { nombreCompleto = value; }
        }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public virtual ObservableCollection<Cuentascliente> Cuentasclientes { get; set; }
        ////public virtual ICollection<Ordene> Ordenes { get; set; }
        //private ObservableCollection<OrdenModelo> ordenes { get; set; }

        //[ManyToOne(CascadeOperations = CascadeOperation.All)]
        //public virtual ObservableCollection<OrdenModelo> Ordenes
        //{
        //    get { return null; }
        //    set { ordenes = value; }
        //}
    }
}
