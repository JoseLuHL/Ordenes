using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture.ViewModels;

namespace XFFurniture.Views.Ordenes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisOrdenes : ContentPage
    {
        public MisOrdenes()
        {
            InitializeComponent();
            BindingContext = new MisReservasViewModel(Navigation);
        }
    }
}