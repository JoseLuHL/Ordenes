using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QP_Comercio_Electronico.Models
{
    public partial class Tipocuentum
    {
        public int TipcuId { get; set; }
        public string TipcuDescripcion { get; set; }

        private ObservableCollection<Cuentascliente> cuentasclientes { get; set; }
        public virtual ObservableCollection<Cuentascliente> Cuentasclientes
        {
            get { return null; }
            set { cuentasclientes = value; }
        }
    }
}
