using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFFurniture.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoPage : ContentPage
    {
        XFFurniture.ViewModels.MainPageViewModel MainPageView => ((XFFurniture.ViewModels.MainPageViewModel)BindingContext);
        public PagoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (MainPageView.ClienteUsuario == null)
            {
                MainPageView.load();
            }
            base.OnAppearing();
        }
    }
}