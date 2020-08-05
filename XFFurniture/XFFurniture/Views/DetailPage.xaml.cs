using SwipeMenu.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture.Models;
using XFFurniture.ViewModels;

namespace XFFurniture.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        MainPageViewModel MainPageViewModel => ((MainPageViewModel)BindingContext);
        public DetailPage()
        {
            InitializeComponent();
            //BindingContext = new DetailPageViewModel(Navigation, product);
        }

        protected override bool OnBackButtonPressed()
        {
            MainPageViewModel.Pagina = 0;
            return base.OnBackButtonPressed();
        }
        //protected override void o()
        //{
        //    MainPageViewModel.Pagina=0
        //    base.OnAppearing();
        //}
    }
}