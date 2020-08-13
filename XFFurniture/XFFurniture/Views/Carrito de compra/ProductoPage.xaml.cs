using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture.ViewModels;

namespace XFFurniture.Views.Carrito_de_compra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoPage : ContentPage
    {
        MainPageViewModel MainPage => ((MainPageViewModel)BindingContext);
        public ProductoPage()
        {
            InitializeComponent();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainPage.IsBusy = true;
            await Task.Delay(200);
            await MainPage.BuscarProductoChanged();
            MainPage.IsBusy = false;

        }
    }
}