using System;
using System.Collections.Generic;
using System.Text;
using XFFurniture.ViewModel;

namespace XFFurniture.Models
{
    public class TiendaModelo : BaseViewModel
    {
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
        public string TienAltura { get; set; }
        public bool? TienPremium { get; set; }


        private string _backgroundColor;
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
