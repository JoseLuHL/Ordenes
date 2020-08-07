using SwipeMenu.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFFurniture;
using XFFurniture.Models;
using XFFurniture.Service;
using XFFurniture.ViewModel;

namespace SwipeMenu.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private bool _areCredentialsInvalid;

        public LoginViewModel(INavigation navigation)
        {
            //IsBusy = true;
            Navigation = navigation;
            AreCredentialsInvalid = false;
        }

        public ICommand PaginaRegistrarCommand => new Command(execute: async () =>
        {
            await Navigation.PushModalAsync(new MisDatosPage());
        });

        private async Task<bool> UserAuthenticated(string username, string password)
        {
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password))
            {
                return false;
            }
            var res = await DataService.GetClienteAsync($"{UrlModelo.cliente}/{username}/{password}");
            if (res != null)
            {
                res.ClieClave = string.Empty;
                UsuarioServicio.Cliente = res;
            }
            return res != null;
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value;
                AreCredentialsInvalid = false;
                SetProperty(ref _username, value);
            }
        }
        public string Password
        {
            get => _password;
            set
            {

                if (value == _password) return;
                _password = value;
                AreCredentialsInvalid = false;
                SetProperty(ref _password, value);
            }
        }

        public ICommand AuthenticateCommand => new Command(execute: async () =>
            {
                IsBusy = true;
                AreCredentialsInvalid = !await UserAuthenticated(Username, Password);
                if (AreCredentialsInvalid==true)
                {
                    AreCredentialsInvalid = true;
                    _areCredentialsInvalid = true;
                    IsBusy = false;
                    return;
                }

                _username = string.Empty;
                _password = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                //MainPage = new HamburgerMenu.HamburgerMenu();
                //await Navigation.PushAsync(new MainPage());
                await Navigation.PushModalAsync(new HamburgerMenu.HamburgerMenu());
                IsBusy = false;
            });

        public bool AreCredentialsInvalid
        {
            get => _areCredentialsInvalid;
            set
            {
                SetProperty(ref _areCredentialsInvalid, value);
            }
        }
    }
}
