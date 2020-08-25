using SwipeMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture;
using XFFurniture.Service;

namespace SwipeMenu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisDatosPage : ContentPage
    {
        MisDatosViewModel misdatos => ((MisDatosViewModel)BindingContext);
        //XFFurniture.Models.TiendaModelo Tienda => ((XFFurniture.Models.TiendaModelo)BindingContext);
        public MisDatosPage()
        {
            InitializeComponent();
            BindingContext = new MisDatosViewModel(Navigation);
        }
        protected async override void OnAppearing()
        {
           //await misdatos.Task();
            //if (Tienda.Tienda.TienLatitud!=null && Tienda.Tienda.TienLongitud!=null)
            //{
            //    miubicacion.IsChecked = true;
            //}


            //if (dat[0] == null)
            //{
            //misdatos.ClieIdentificacion = dat[0].ClieIdentificacion;
            //    ClieNombre = Cliente.ClieNombre;
            //    ClieApellidos = Cliente.ClieApellidos;
            //    ClieClave = Cliente.ClieClave;
            //    ClieTelefono = Cliente.ClieTelefono;
            //    ClieDireccion = Cliente.ClieDireccion;
            //    Latitud = Cliente.ClieLatitud;
            //    Longitud = Cliente.ClieLongitud;
            //}

            base.OnAppearing();
        }
    }
}