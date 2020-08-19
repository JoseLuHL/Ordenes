using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QP_Comercio_Electronico.Models
{
    public partial class Banco
    {

        public int BanId { get; set; }
        public string BanDescripcion { get; set; }

        public virtual ObservableCollection<Cuentascliente> Cuentasclientes { get; set; }
    }
}
