using SwipeMenu.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFFurniture.Interfaces;
using XFFurniture.Models;
using XFFurniture.ViewModel;

namespace XFFurniture.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {
        public DetailPageViewModel(INavigation navigation, ProductoModelo product)
        {
            Navigation = navigation;
            DependencyService.Get<IStatusBarStyle>().ChangeTextColor(true);
            PopDetailPageCommand = new Command(async () => await ExecutePopDetailPageCommand());
            Product = product;
            Product = null;
        }

        public Command PopDetailPageCommand { get; }
        //public ProductoModelo Product { get; set; }
        private ProductoModelo product;

        public ProductoModelo Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }


        private async Task ExecutePopDetailPageCommand()
        {
            await Navigation.PopAsync();
        }
    }
}
