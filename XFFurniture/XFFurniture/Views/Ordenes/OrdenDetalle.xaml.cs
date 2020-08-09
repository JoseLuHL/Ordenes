using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture.ViewModels;

namespace AgendaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdenDetalle : ContentPage
    {
        MainPageViewModel MainView => ((MainPageViewModel)BindingContext);
        public OrdenDetalle()
        {
            InitializeComponent();
            
        }
        protected override void OnAppearing()
        {
            listCategories.ItemsSource = MainView.OrdenSelect.Ordendetalles;
            base.OnAppearing();
        }

    }
}