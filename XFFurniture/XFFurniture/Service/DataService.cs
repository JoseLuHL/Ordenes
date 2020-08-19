using Newtonsoft.Json;
using QP_Comercio_Electronico.Models;
using SwipeMenu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFFurniture.Models;
using System.Drawing;
namespace XFFurniture.Service
{
    public class DataService
    {
        static HttpClient Http;

        public static string GenerarCodigoVerificacion()
        {
            List<long> numeros10Digitos = new List<long>();
            long numeroAleatorio = 0;
            do
            {
                numeroAleatorio = Convert.ToInt64(
                    $"{new Random().Next(10, 49)}{new Random().Next(50, 99)}");
            } while (numeros10Digitos.Contains(numeroAleatorio));
            numeros10Digitos.Add(numeroAleatorio);
            return numeroAleatorio.ToString();
        }

        public static ObservableCollection<Category> GetCategories()
        {
            return new ObservableCollection<Category>()
            {
                new Category()
                {
                    description = "Chairs",
                    numberItems = 4512,
                    image = "armchair.png",
                    backgroundColor = "#EAEDF6",
                    textColor = "#000000"
                },
                new Category()
                {
                    description = "Lamps",
                    numberItems = 512,
                    image = "lamp.png",
                    backgroundColor = "#EAEDF6",
                    textColor = "#000000"
                },
                new Category()
                {
                    description = "TV Stand",
                    numberItems = 1815,
                    image = "tvstand.png",
                    backgroundColor = "#EAEDF6",
                    textColor = "#000000"
                },
            };
        }

        public static async Task<ObservableCollection<TiendaModelo>> GetTiendaModelosAsync()
        {
            var respuesta = new ObservableCollection<TiendaModelo>();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(UrlModelo.tiendas);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<TiendaModelo>>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }

        public static async Task<ObservableCollection<OrdenModelo>> GetOrdenModelosAsync(string url)
        {
            var respuesta = new ObservableCollection<OrdenModelo>();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(url);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<OrdenModelo>>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }

        public static async Task<ObservableCollection<T>> GetsAsync<T>(string url)
        {
            Http = new HttpClient();
            var retornar = new ObservableCollection<T>();
            var conte = await Http.GetAsync(url);
            try
            {
                if (conte.IsSuccessStatusCode)
                {
                    var resp = await conte.Content.ReadAsStringAsync();
                    retornar = JsonConvert.DeserializeObject<ObservableCollection<T>>(resp);
                }
                //else
                //    Error = conte.IsSuccessStatusCode.ToString();
            }
            catch (Exception ex)
            {
                //Error = ex.Message;
                await Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
            return retornar;
        }
        public static async Task<T> GetAsync<T>(string url)
        {
            Http = new HttpClient();
            var retornar = new ObservableCollection<T>();
            var conte = await Http.GetAsync(url);
            try
            {
                if (conte.IsSuccessStatusCode)
                {
                    var resp = await conte.Content.ReadAsStringAsync();
                    var retornar1 = JsonConvert.DeserializeObject<T>(resp);
                    retornar.Add(retornar1);
                }
                //else
                //    Error = conte.IsSuccessStatusCode.ToString();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
            return retornar[0];
        }

        public static async Task<ObservableCollection<ClienteModelo>> GetClientesAsync(string url)
        {
            var respuesta = new ObservableCollection<ClienteModelo>();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(url);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<ClienteModelo>>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }
        public static async Task<ClienteModelo> GetClienteAsync(string url)
        {
            var respuesta = new ClienteModelo();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(url);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ClienteModelo>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }

        public static async Task<ObservableCollection<TiendaModelo>> GetTiendasAsync(string urlTienda)
        {
            var respuesta = new ObservableCollection<TiendaModelo>();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(urlTienda);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<TiendaModelo>>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }
        public static async Task<ObservableCollection<ProductoModelo>> GetProductoAsync(string urlProductos)
        {
            var respuesta = new ObservableCollection<ProductoModelo>();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(urlProductos);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<ProductoModelo>>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }
        public static async Task<ObservableCollection<Mediopago>> GetMedioPagoAsync(string urlCategoria)
        {
            var respuesta = new ObservableCollection<Mediopago>();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(urlCategoria);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Mediopago>>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }
        public static async Task<ObservableCollection<Categoria>> GetCategoriaAsync(string urlCategoria)
        {
            var respuesta = new ObservableCollection<Categoria>();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(urlCategoria);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Categoria>>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }

        public static async Task<TiendaModelo> GetTiendasOneAsync(string urlTienda)
        {
            var respuesta = new TiendaModelo();
            Http = new HttpClient();
            var peticion = await Http.GetAsync(urlTienda);
            if (peticion.IsSuccessStatusCode)
            {
                var contenido = await peticion.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<TiendaModelo>(contenido);
                respuesta = datos;
            }
            else
                respuesta = null;
            return respuesta;
        }

        public static async Task<string> PostGuardarAsync<T>(T datos, string url)
        {
            Http = new HttpClient();
            var retornar = "";
            var resp = "";
            var json = JsonConvert.SerializeObject(datos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var peti = await Http.PostAsync(url, content);
            if (peti.IsSuccessStatusCode)
            {
                retornar = peti.IsSuccessStatusCode.ToString();
                var conte = await peti.Content.ReadAsStringAsync();
                resp = conte;
                retornar = conte;
                //resp = JsonConvert.DeserializeObject<string>(conte);
            }
            else
                retornar = "";
            return retornar;
        }
        
        public static async Task<bool> PutActualizarAsync<T>(T datos, string url)
        {
            Http = new HttpClient();
            var retornar = false;
            var resp = "";
            var json = JsonConvert.SerializeObject(datos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var peti = await Http.PostAsync(url, content);
            if (peti.IsSuccessStatusCode)
            {
                retornar = peti.IsSuccessStatusCode;
                var conte = await peti.Content.ReadAsStringAsync();
                resp = conte;
                //resp = JsonConvert.DeserializeObject<string>(conte);
            }
            else
                retornar = false;

            if (resp == "OK")
            {
                retornar = true;
            }

            return retornar;
        }
    }
}
