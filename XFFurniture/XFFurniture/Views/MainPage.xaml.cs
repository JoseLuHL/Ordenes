using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFFurniture.Interfaces;
using XFFurniture.ViewModels;

namespace XFFurniture
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel MainPageViewModel => ((MainPageViewModel)BindingContext);
        int x = 0;
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = new MainPageViewModel(Navigation);
            x++;
        }

        protected async override void OnAppearing()
        {
            if (x == 1)
            {
                MainPageViewModel.IsLoad = true;
                MainPageViewModel.IsCargando = false;
                await Task.Delay(1000);
                //while (MainPageViewModel.IsBusy==true)
                //{
                //await Task.Delay(80);                    
                //}
                MainPageViewModel.IsLoad = false;
                MainPageViewModel.IsCargando = true;
            }
            x++;
            DependencyService.Get<IStatusBarStyle>().ChangeTextColor();
        }
    }
}
