using MvvmHelpers;
using SwipeMenu.Models;
using SwipeMenu.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFFurniture;
using XFFurniture.Models;
using XFFurniture.Service;


namespace SwipeMenu.ViewModel
{
    public class MisDatosViewModel : BaseViewModel
    {

        private string latitud;
        public string Latitud
        {
            get => latitud;
            set => SetProperty(ref latitud, value);
        }
        private string longitud;
        public string Longitud
        {
            get => longitud;
            set => SetProperty(ref longitud, value);
        }
        private string mensajeUbicacion;
        public string MensajeUbicacion
        {
            get => mensajeUbicacion;
            set => SetProperty(ref mensajeUbicacion, value);
        }

        private string colorUbicacion = "Red";
        public string ColorUbicacion
        {
            get => colorUbicacion;
            set => SetProperty(ref colorUbicacion, value);
        }
        public ICommand UbicacionCommand => new Command(async () =>
        {
            IsBusy = true;
            var ubicacion = new UbicacionServicio();
            var ubi = await ubicacion.OnGetCurrentLocation();
            if (string.IsNullOrEmpty(ubi))
            {
                await Application.Current.MainPage.DisplayAlert("", "No podemos optener su ubicación \n " +
                    "verifique si esta activado el GPS", "OK");
                MensajeUbicacion = "Sin ubicacion";
                ColorUbicacion = "Red";
                IsBusy = false;
                return;
            }
            var array = ubi.Split(' ');
            Latitud = array[0];
            Longitud = array[1];
            ClieLatitud = Latitud;
            ClieLongitud = Longitud;
            ColorUbicacion = "Green";
            MensajeUbicacion = "Ubicación obtenida";
            IsBusy = false;

        });

        private async Task GetCliente()
        {
            var cli = new ClienteModelo();
            if (string.IsNullOrEmpty(ClieIdentificacion))
            {
                await Application.Current.MainPage.DisplayAlert("", "Ingrese una identificación", "OK");
                return;
            }
            if (string.IsNullOrEmpty(ClieNombre))
            {
                await Application.Current.MainPage.DisplayAlert("", "Ingrese su nombre", "OK");
                return;
            }
            if (string.IsNullOrEmpty(ClieApellidos))
            {
                await Application.Current.MainPage.DisplayAlert("", "Ingrese sus apellidos", "OK");
                return;
            }
            if (string.IsNullOrEmpty(ClieClave))
            {
                await Application.Current.MainPage.DisplayAlert("", "Ingrese una clave", "OK");
                return;
            }
            if (string.IsNullOrEmpty(ClieDireccion))
            {
                await Application.Current.MainPage.DisplayAlert("", "Ingrese una dirección", "OK");
                return;
            }
            if (string.IsNullOrEmpty(ClieTelefono))
            {
                await Application.Current.MainPage.DisplayAlert("", "Ingrese sus telefono", "OK");
                return;
            }
            //if (string.IsNullOrEmpty(ClieLatitud))
            //{
            //    await Application.Current.MainPage.DisplayAlert("", "Ingrese sus telefono", "OK");
            //    return;
            //}

            cli.ClieIdentificacion = ClieIdentificacion;
            cli.ClieNombre = ClieNombre;
            cli.ClieTelefono = ClieTelefono;
            cli.ClieApellidos = ClieApellidos;
            cli.ClieDireccion = ClieDireccion;
            cli.ClieClave = ClieClave;
            cli.ClieLongitud = ClieLongitud;
            cli.ClieLatitud = ClieLatitud;

            try
            {

             var guardar = await DataService.PostGuardarAsync<ClienteModelo>(cli,UrlModelo.cliente);
                if (guardar)
                {
                    var ope = await App.SQLiteDb.SaveItemAsync(cli);
                    await Application.Current.MainPage.DisplayAlert("", "Los datos se han guardado", "OK");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }else
                    await Application.Current.MainPage.DisplayAlert("", "Los datos no se pudieron guardar \n vuelva a intentarlo", "OK");



            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                //throw;
            }
        }

        private string clieIdentificacion;
        public string ClieIdentificacion
        {
            get => clieIdentificacion;
            set { SetProperty(ref clieIdentificacion, value); }
        }

        private string clieNombre;
        public string ClieNombre
        {
            get => clieNombre;
            set { SetProperty(ref clieNombre, value); }
        }

        private string clieApellidos;
        public string ClieApellidos
        {
            get => clieApellidos;
            set { SetProperty(ref clieApellidos, value); }
        }

        private string clieClave;
        public string ClieClave
        {
            get => clieClave;
            set { SetProperty(ref clieClave, value); }
        }
        private string clieTelefono;

        public string ClieTelefono
        {
            get => clieTelefono;
            set { SetProperty(ref clieTelefono, value); }
        }
        private string clieDireccion;

        public string ClieDireccion
        {
            get => clieDireccion;
            set { SetProperty(ref clieDireccion, value); }
        }
        private string clieLatitud;

        public string ClieLatitud
        {
            get => clieLatitud;
            set { SetProperty(ref clieLatitud, value); }
        }
        private string clieLongitud;

        public string ClieLongitud
        {
            get => clieLongitud;
            set { SetProperty(ref clieLongitud, value); }
        }

        public ICommand GuardarCommand => new Command(execute: async () =>
       {
           IsBusy = true;
           IsNotBusy = false;
           await GetCliente();
           IsBusy = false;
           IsNotBusy = true;

       });

        private ClienteModelo cliente;

        public ClienteModelo Cliente
        {
            get => Cliente;
            set
            {
                SetProperty(ref cliente, value);
            }
        }


        public MisDatosViewModel()
        {
            //Navigation = navigation;
            Cliente = UsuarioServicio.Cliente;
            ColorUbicacion = "Red";
            MensajeUbicacion = "Sin ubicacion";
        }



    }
}
