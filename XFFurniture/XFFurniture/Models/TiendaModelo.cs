using SQLite;
using SQLiteNetExtensions.Attributes;
using SwipeMenu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using XFFurniture.ViewModel;

namespace XFFurniture.Models
{
    public class TiendaModelo : BaseViewModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int TienId { get; set; }
        public string TienNit { get; set; }
        public string TienTipoidentificacion { get; set; }
        public string TienRazonsocial { get; set; }
        public string TienDireccion { get; set; }
        public string TienBarrio { get; set; }
        public string TienTelefono { get; set; }
        public string TienCorreo { get; set; }
        public double TienLatitud { get; set; }
        public double TienLongitud { get; set; }
        public string TienFacebook { get; set; }
        public string TienInstagram { get; set; }
        public string TienYoutube { get; set; }
        public string TienFoto { get; set; }
        public string TienAltura { get; set; }
        public bool? TienPremium { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public virtual ICollection<Detalletiendacategorium> Detalletiendacategoria { get; set; }
        private ICollection<OrdenModelo> ordenes { get; set; }
        [Ignore]
        public virtual ICollection<OrdenModelo> Ordenes
        {
            get { return ordenes; }
            set { ordenes = value; }
        }
        //public virtual ICollection<ProductoModelo> Productos { get; set; }

        private string _backgroundColor= "#EAEDF6";
        public string backgroundColor
        {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }

        private string _textColor;
        public string textColor
        {
            get { return _textColor; }
            set { SetProperty(ref _textColor, value); }
        }

        private bool _selected;
        public bool selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }



    }
}
