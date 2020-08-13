using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using XFFurniture.Models;
using XFFurniture.ViewModel;

namespace SwipeMenu.Models
{
    public class ProductoModelo : BaseViewModel
    {
        //public ProductoModelo()
        //{
        //    this.colors = new List<XFFurniture.Models.Color>();
        //}
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int ProdId { get; set; }
        public string ProdCodigo { get; set; }
        public string ProdNombre { get; set; }
        public string ProdDescripcion { get; set; }
        public double? ProdPrecioanterior { get; set; }
        //public int? Cantidad { get; set; }
        private int cantidad;

        public int Cantidad
        {
            get => cantidad;
            set
            {
                SetProperty(ref cantidad, value);
            }
        }


        public double ProdPreciounitario { get; set; }
        public string ProdFoto { get; set; }
        public string ProdColor { get; set; }
        public string ProdRanking { get; set; }
        public int? ProdStockmin { get; set; }
        public int? ProdStok { get; set; }
        public int? ProdStokmax { get; set; }
        public string ProdFecha { get; set; }
        [Ignore]
        public List<XFFurniture.Models.Color> colors { get; set; }
        public double? ProdCalificacion { get; set; }
        public int ProdCountventas { get; set; }
        public bool? ProdFavorito { get; set; }

        //[ManyToOne(CascadeOperations = CascadeOperation.All)]
        //public  Categoria ProdCategoriaNavigation { get; set; }
        //public virtual ICollection<Ordendetalle> Ordendetalles { get; set; }
        //[Ignore]
        //private ICollection<Ordendetalle> ordendetalles { get; set; }
        //[Ignore]
        //public virtual ICollection<Ordendetalle> Ordendetalles
        //{
        //    get { return null; }
        //    set { ordendetalles = value; }
        //}

        public int? ProdIdcategoria { get; set; }
        public double? ProdDescuento { get; set; }
        public int? ProdIdtienda { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public TiendaModelo ProdIdtiendaNavigation { get; set; }
        [Ignore]
        public  Subcategorium ProdIdcategoriaNavigation { get; set; }
    }
}
