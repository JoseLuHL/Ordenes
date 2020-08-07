using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFFurniture.ViewModel;

namespace SwipeMenu.Service
{
    class UbicacionServicio:BaseViewModel
    {
        string notAvailable = "no disponible";
        string lastLocation;
        string currentLocation;
        string latitud;
        string logitud;
        int accuracy = (int)GeolocationAccuracy.Default;
        CancellationTokenSource cts;

        public UbicacionServicio( )
        {
            //Navigation = navigation;
            GetLastLocationCommand = new Command(OnGetLastLocation);
            //GetCurrentLocationCommand = new Command(OnGetCurrentLocation);
        }

        public ICommand GetLastLocationCommand { get; }

        public ICommand GetCurrentLocationCommand { get; }

        public string LastLocation
        {
            get => lastLocation;
            set => SetProperty(ref lastLocation, value);
        }

        public string CurrentLocation
        {
            get => currentLocation;
            set => SetProperty(ref currentLocation, value);
        }
        //public string Latitud
        //{
        //    get => latitud;
        //    set => SetProperty(ref latitud, value);
        //}
        //public string Logintud
        //{
        //    get => logitud;
        //    set => SetProperty(ref logitud, value);
        //}

        public string Logintud { get; set; }
        public string Latitud { get; set; }
        public string[] Accuracies
            => Enum.GetNames(typeof(GeolocationAccuracy));

        public int Accuracy
        {
            get => accuracy;
            set => SetProperty(ref accuracy, value);
        }

        public async void OnGetLastLocation()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                LastLocation = FormatLocation(location);
                Latitud = GetLatitud(location);
                Logintud = GetLongitud(location);
            }
            catch (Exception ex)
            {
                LastLocation = FormatLocation(null, ex);
            }
            IsBusy = false;
        }

        public  async Task<string>  OnGetCurrentLocation()
        {
            //if (IsBusy)
            //    return;
            string datos = "";
            IsBusy = true;
            try
            {
                var request = new GeolocationRequest((GeolocationAccuracy)Accuracy);
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                CurrentLocation = FormatLocation(location);
                Latitud = GetLatitud(location);
                Logintud = GetLongitud(location);
                datos = $"{Latitud} {Logintud}";
            }
            catch (Exception ex)
            {
                CurrentLocation = FormatLocation(null, ex);
            }
            finally
            {
                cts.Dispose();
                cts = null;
            }
            IsBusy = false;
            return datos;
        }

        string FormatLocation(Location location, Exception ex = null)
        {
            if (location == null)
            {
                return $"No se puede detectar la ubicación. Excepción: {ex?.Message ?? string.Empty}";
            }

            return
                $"Latitud: {location.Latitude}\n" +
                $"Longitud: {location.Longitude}\n" +
                $"Exactitud: {location.Accuracy}\n" +
                $"Altitud: {(location.Altitude.HasValue ? location.Altitude.Value.ToString() : notAvailable)}\n" +
                $"Titulo: {(location.Course.HasValue ? location.Course.Value.ToString() : notAvailable)}\n" +
                $"Velocidad: {(location.Speed.HasValue ? location.Speed.Value.ToString() : notAvailable)}\n" +
                $"Fecha (UTC): {location.Timestamp:d}\n" +
                $"Hora (UTC): {location.Timestamp:T}" +
                $"Moking Provider: {location.IsFromMockProvider}";
        }
        string GetLatitud(Location location, Exception ex = null)
        {
            if (location == null)
            {
                return $"No se puede detectar la ubicación. Excepción: {ex?.Message ?? string.Empty}";
            }

            return location.Latitude.ToString();


        }
        string GetLongitud(Location location, Exception ex = null)
        {
            if (location == null)
            {
                return $"No se puede detectar la ubicación. Excepción: {ex?.Message ?? string.Empty}";
            }
            return location.Longitude.ToString();
        }

        //public override void OnDisappearing()
        //{
        //    if (IsBusy)
        //    {
        //        if (cts != null && !cts.IsCancellationRequested)
        //            cts.Cancel();
        //    }
        //    base.OnDisappearing();
        //}
    }
}
