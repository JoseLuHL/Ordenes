using SwipeMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture.Service;

namespace SwipeMenu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPagina : ContentPage
    {


        public LoginPagina()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            try
            {
                if (await UsuarioServicio.EstadologinAsync())
                {
                    await Navigation.PopModalAsync();
                }
                base.OnAppearing();
            }
            catch (Exception ex)
            {
               await DisplayAlert("Login", ex.ToString(), "OK");
            }
           
        }
    }
}