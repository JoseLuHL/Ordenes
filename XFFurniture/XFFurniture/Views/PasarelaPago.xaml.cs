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
    public partial class PasarelaPago : ContentPage
    {
        public PasarelaPago()
        {
            InitializeComponent();
            vista.Source = "http://192.168.1.10:8080/prueba/prueba.php?precio=10000&descripcion=Descripcion";
        }

        private void Webview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            
        }

        protected override void OnAppearing()
        {
            vista.Source = "http://192.168.1.10:8080/prueba/prueba.php?precio=10000&descripcion=Descripcion";
            base.OnAppearing();
        }

        private void Webview_Navigated(object sender, WebNavigatedEventArgs e)
        {

        }
    }
}