using SwipeMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwipeMenu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisDatosPage : ContentPage
    {
        //MisDatosViewModel Tienda => ((MisDatosViewModel)BindingContext);
        //XFFurniture.Models.TiendaModelo Tienda => ((XFFurniture.Models.TiendaModelo)BindingContext);
        public MisDatosPage()
        {
            InitializeComponent();
            //BindingContext = new MisDatosViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            //if (Tienda.Tienda.TienLatitud!=null && Tienda.Tienda.TienLongitud!=null)
            //{
            //    miubicacion.IsChecked = true;
            //}
            base.OnAppearing();
        }
    }
}