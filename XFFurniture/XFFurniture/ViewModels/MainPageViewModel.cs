using AgendaApp;
using QP_Comercio_Electronico.Models;
using SwipeMenu.Models;
using SwipeMenu.Service;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingWithMaps;
using WorkingWithMaps.Vistas.Reservas;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XFFurniture.Interfaces;
using XFFurniture.Models;
using XFFurniture.Service;
using XFFurniture.ViewModel;
using XFFurniture.Views;
using XFFurniture.Views.Ordenes;

namespace XFFurniture.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation)
        {
            IsLoad = true;
            IsCargando = false;
            IsBusy = true;
            NoIsBusy = true;
            Navigation = navigation;
            DependencyService.Get<IStatusBarStyle>().ChangeTextColor(true);
            PopDetailPageCommand = new Command(async () => await ExecutePopDetailPageCommand());
            SelectCategoryCommand = new Command<TiendaModelo>(async (param) => await ExecuteSelectCategoryCommand(param));
            NavigateToDetailPageCommand = new Command<ProductoModelo>(async (param) => await ExeccuteNavigateToDetailPageCommand(param));
            SelectProductoCarritoCommand = new Command<ProductoModelo>(async (param) => await ExeccuteNavigateToDetailPageCommand1(param));
            _ = GetTienda();
            _ = GetProducts();
            _ = load();
        }

        public async Task load()
        {
            var dat = await App.SQLiteDb.GetItemsAsync();
            UsuarioServicio.Cliente = dat[0];
            ClienteUsuario = dat[0];
        }

        private ClienteModelo clienteUsuario;

        public ClienteModelo ClienteUsuario
        {
            get => clienteUsuario;
            set
            {
                SetProperty(ref clienteUsuario, value);
            }
        }

        public ICommand CateCommand => new Command(execute: async () =>
         {
             await DisplayAlert("", "Hola", "OK");

         });
        public ICommand ShowAllCommand => new Command(async () =>
        {
            await GetProducts();
        });
        public ICommand CarritoCommand => new Command(async () =>
        {
            if (TiendaCarrito != null && TiendaCarrito.Count > 0)
            {
                await TiendasCarritoAsync();
                //await Navigation.PushModalAsync(new CarritoPage { BindingContext = this });
                await Navigation.PushModalAsync(new TiendaCarritoPage { BindingContext = this });

            }
            else
                await DisplayAlert("!", "No hay productos en el carro", "Ok");
        });
        public ICommand TiendasCommand => new Command(async () =>
        {
            await GetCategories();
            await GetTienda();
            await Navigation.PushModalAsync(new TiendasPage { BindingContext = this });
        });
        public ICommand MapaCommand => new Command(async () =>
        {
            IsBusy = true;
            await GetTienda();
            await Navigation.PushModalAsync(new PinPage { BindingContext = this });
            IsBusy = false;
        });
        public ICommand MisOrdenesCommand => new Command(async () =>
        {
            IsBusy = true;
            await Navigation.PushModalAsync(new MisOrdenes());
            IsBusy = false;
        });
        public ICommand CategoriasCommand => new Command(async () =>
        {
            await GetCategories();
            await Navigation.PushModalAsync(new CategoriaPage { BindingContext = this });
        });
        public ICommand AnadirCarrito => new Command(async () =>
        {
            try
            {
                var tiendaCarrito_ = new ObservableCollection<ProductoModelo>();
                var CarritoAn = new ObservableCollection<ProductoModelo>();
                CarritoAn = TiendaCarrito;
                if (TiendaCarrito != null)
                {
                    var buscar = TiendaCarrito.Where(s => s.ProdId == ProductoDet.ProdId).ToList();
                    if (buscar.Count > 0)
                    {
                        CarritoAn.Remove(buscar[0]);
                        tiendaCarrito_.Add(ProductoDet);
                    }
                    else
                        tiendaCarrito_.Add(ProductoDet);
                }
                else
                {
                    tiendaCarrito_.Add(ProductoDet);
                }

                if (CarritoAn != null)
                {
                    foreach (var item in CarritoAn)
                    {
                        tiendaCarrito_.Add(item);
                    }
                }
                ProductoDet.Cantidad++;
                TiendaCarrito = tiendaCarrito_;
                CalcularTotal();
                TotalCarrito = TiendaCarrito.Count;
            }
            catch (System.Exception ex)
            {

                await DisplayAlert("", ex.ToString(), "OK");
            }

        });
        public ICommand QuitarCarrito => new Command(async () =>
        {
            try
            {
                var tiendaCarrito_ = new ObservableCollection<ProductoModelo>();
                var CarritoAn = new ObservableCollection<ProductoModelo>();
                CarritoAn = TiendaCarrito;

                if (TiendaCarrito != null)
                {
                    var buscar = TiendaCarrito.Where(s => s.ProdId == ProductoDet.ProdId).ToList();
                    if (buscar.Count > 0)
                    {
                        ProductoDet.Cantidad--;
                        CarritoAn.Remove(buscar[0]);
                        if (ProductoDet.Cantidad > 0)
                        {
                            tiendaCarrito_.Add(ProductoDet);
                        }
                        else
                            ProductoDet.Cantidad = 0;
                    }
                }

                //    await DisplayAlert("", "El producto no se ha agregado", "OK");

                if (CarritoAn != null)
                {
                    foreach (var item in CarritoAn)
                    {
                        tiendaCarrito_.Add(item);
                    }
                }

                TiendaCarrito = tiendaCarrito_;
                CalcularTotal();
                TotalCarrito = TiendaCarrito.Count;
                //totalCarrito = TiendaCarrito.Count;
                //ProductoDet = null;
                //await DisplayAlert("", "", "OK");

            }
            catch (System.Exception ex)
            {

                await DisplayAlert("", ex.ToString(), "OK");
            }

        });
        public Command NavigateToDetailPageCommand { get; set; }
        public Command SelectProductoCarritoCommand { get; set; }
        public Command SelectCategoryCommand { get; set; }
        public Command PopDetailPageCommand { get; }

        private ObservableCollection<ProductoModelo> tiendaCarrito;
        public ObservableCollection<ProductoModelo> TiendaCarrito
        {
            get => tiendaCarrito;
            set
            {
                SetProperty(ref tiendaCarrito, value);
                //SetProperty(ref totalCarrito, TiendaCarrito.Count);
                TotalCarrito = tiendaCarrito.Count;
                //totalCarrito = tiendaCarrito.Count;
            }
        }
        public ObservableCollection<Category> Categories { get; set; }
        private ObservableCollection<Categoria> categorias;
        public ObservableCollection<Categoria> Categorias
        {
            get => categorias;
            set => SetProperty(ref categorias, value);
        }
        private ObservableCollection<Subcategorium> subCategorias;
        public ObservableCollection<Subcategorium> SubCategorias
        {
            get => subCategorias;
            set => SetProperty(ref subCategorias, value);

        }
        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }
        private ObservableCollection<TiendaModelo> tiendas;
        public ObservableCollection<TiendaModelo> Tiendas
        {
            get => tiendas;
            set => SetProperty(ref tiendas, value);
        }
        private ObservableCollection<TiendaModelo> tiendasPremun;
        public ObservableCollection<TiendaModelo> TiendasPremiun
        {
            get => tiendasPremun;
            set => SetProperty(ref tiendasPremun, value);
        }
        private ObservableCollection<ProductoModelo> productos;
        public ObservableCollection<ProductoModelo> Productos
        {
            get => productos;
            set => SetProperty(ref productos, value);
        }
        private ProductoModelo productoDet;
        public ProductoModelo ProductoDet
        {
            get => productoDet;
            set => SetProperty(ref productoDet, value);
        }
        private double totalCompra;
        public double TotalCompra
        {
            get => totalCompra;
            set
            {
                SetProperty(ref totalCompra, value);
            }
        }
        private double totalCompraDetalle;
        public double TotalCompraDetalle
        {
            get => totalCompraDetalle;
            set
            {
                SetProperty(ref totalCompraDetalle, value);
            }
        }
        private int totalCarrito;
        public int TotalCarrito
        {
            get => totalCarrito;
            set
            {
                SetProperty(ref totalCarrito, value);
            }
        }
        private int pagina;
        public int Pagina
        {
            get => pagina;
            set
            {
                SetProperty(ref pagina, value);
            }
        }

        private System.DateTime fechaOrden = System.DateTime.Now;

        public System.DateTime FechaOrden
        {
            get => System.DateTime.Now;
            set
            {
                SetProperty(ref fechaOrden, value);
            }
        }

        private string tipoEstimado = "Sin información";

        public string TipoEstimado
        {
            get => tipoEstimado;
            set
            {
                SetProperty(ref tipoEstimado, value);
            }
        }

        //CORRESPONDIENTE A LAS ORDENES Y DETALLE
        private ObservableCollection<OrdenModelo> ordenes;
        public ObservableCollection<OrdenModelo> Ordenes
        {
            get => ordenes;
            set
            {
                SetProperty(ref ordenes, value);
            }
        }
        public ICommand OrdenesCommand => new Xamarin.Forms.Command(async () =>
        {
            try
            {
                await GetOrdenesAsync();
                if (Ordenes.Count < 1)
                {
                    await Application.Current.MainPage.DisplayAlert("", "No hay pedidos", "OK");
                    IsBusy = false;
                    return;
                }
                await Navigation.PushModalAsync(new OrdenesPage { BindingContext = this });
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("", ex.ToString(), "OK");
            }
        });
        public ICommand RefrescarOrdenesComman => new Command(execute: async () =>
        {
            IsBusy = true; await GetOrdenesAsync(); IsBusy = false;
        });
        private OrdenModelo ordenSelect;
        public OrdenModelo OrdenSelect
        {
            get => ordenSelect;
            set => SetProperty(ref ordenSelect, value);
        }
        private ObservableCollection<Ordendetalle> ordenDetalle;
        public ObservableCollection<Ordendetalle> OrdenDetalle
        {
            get => ordenDetalle;
            set => SetProperty(ref ordenDetalle, value);
        }
        public ICommand SelectOrdenCommand => new Command<OrdenModelo>(async (OrdenModelo modelo) =>
        {
            OrdenSelect = modelo;
            OrdenDetalle = modelo.Ordendetalles;
            modelo = null;
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdenDetalle { BindingContext = this });
        });
        async Task GetOrdenesAsync()
        {
            IsBusy = true;
            Ordenes = await DataService.GetOrdenModelosAsync($"{UrlModelo.odenes}");
            IsBusy = false;
        }
        //:::::::::::::Fin ordenes y detalle


        //-----------------------------------------
        //FORMA DE PAGO
        private ObservableCollection<Mediopago> mediopagos;
        public ObservableCollection<Mediopago> Mediopagos
        {
            get => mediopagos;
            set
            {
                SetProperty(ref mediopagos, value);
            }
        }
        async Task GetMedioPagoAsync()
        {
            Mediopagos = await DataService.GetMedioPagoAsync(UrlModelo.formaPago);
        }
        //::::::::::Fin forma de pago


        //PAGAR
        private string urlPago;
        public string UrlPago
        {
            get => urlPago;
            set
            {
                SetProperty(ref urlPago, value);
            }
        }

        public ICommand PagarCommand => new Command(execute: async () =>
         {
             await GetMedioPagoAsync();
             await Navigation.PushModalAsync(new PagoPage { BindingContext = this });
         });
        public ICommand PasarelaPagoCommand => new Command<Mediopago>(execute: async (medio) =>
          {
              IsBusy = true;
              NoIsBusy = false;
              if (!await UsuarioServicio.EstadologinAsync())
              {
                  await Navigation.PushModalAsync(new SwipeMenu.Views.LoginPagina());
                  IsBusy = false;
                  NoIsBusy = true;
                  return;
              }


              await PasarelaAsync(medio);

          });
        async Task PasarelaAsync(Mediopago mediopago)
        {
            IsBusy = true;
            NoIsBusy = false;
            if (mediopago.MepDescripcion == "Pagado")
            {
                UrlPago = "http://192.168.1.10:8080/prueba/prueba.php?precio=10000&descripcion=Descripcion";
                await Navigation.PushModalAsync(new PagosVista { BindingContext = this });
                IsBusy = false;
                NoIsBusy = true;
                return;
            }

            await OrdenClienteAsycn(mediopago.MepId);

            IsBusy = false;
            NoIsBusy = true;
            //await DisplayAlert("", mediopago.MepDescripcion, "OK");
        }
        //:::::::::: Fin de pagar


        //PAGINA DE TIENDACARRITO
        private ObservableCollection<ProductoModelo> tiendaCarritoDetalle;
        public ObservableCollection<ProductoModelo> TiendaCarritoDetalle
        {
            get => tiendaCarritoDetalle;
            set
            {
                SetProperty(ref tiendaCarritoDetalle, value);
                //SetProperty(ref totalCarrito, TiendaCarrito.Count);
                //TotalCarrito = tiendaCarrito.Count;
                //totalCarrito = tiendaCarrito.Count;
            }
        }

        private ObservableCollection<TiendaModelo> tiendasCarrito;
        public ObservableCollection<TiendaModelo> TiendasCarrito
        {
            get => tiendasCarrito;
            set
            {
                SetProperty(ref tiendasCarrito, value);
            }
        }
        public ICommand TiendasCarritoCommand => new Command<TiendaModelo>(execute: async (tienda) =>
        {
            var dat = TiendaCarrito.Where(s => s.ProdIdtiendaNavigation.TienId == tienda.TienId);
            TiendaCarritoDetalle = new ObservableCollection<ProductoModelo>(dat);
            CalcularTotalDetalle();
            await Navigation.PushModalAsync(new CarritoPage { BindingContext = this });
        });
        async Task TiendasCarritoAsync()
        {
            var tien = new ObservableCollection<TiendaModelo>();
            if (TiendaCarrito != null && TiendaCarrito.Count > 0)
            {
                foreach (var item in TiendaCarrito)
                {
                    tien.Add(item.ProdIdtiendaNavigation);
                }
                TiendasCarrito = new ObservableCollection<TiendaModelo>(tien.GroupBy(s => s.TienId).Select(g => g.First()));
            }
        }
        //::::::::::::: Fin tiendacarrito

        //PARA LA ORDEN O EL PEDIDO
        private OrdenModelo ordenModelo;
        public OrdenModelo OrdenModelo
        {
            get => ordenModelo;
            set
            {
                SetProperty(ref ordenModelo, value);
            }
        }
        private ObservableCollection<Ordendetalle> ordendetalles;
        public ObservableCollection<Ordendetalle> Ordendetalles
        {
            get => ordendetalles;
            set
            {
                SetProperty(ref ordendetalles, value);
            }
        }

        private string descripcion;
        public string Descripcion
        {
            get => descripcion;
            set
            {
                SetProperty(ref descripcion, value);
            }
        }
        private string direccion;
        public string Direccion
        {
            get => direccion;
            set
            {
                SetProperty(ref direccion, value);
            }
        }

        async Task OrdenClienteAsycn(int idmediopago)
        {
            if (!await UsuarioServicio.EstadologinAsync())
            {
                await Navigation.PushModalAsync(new SwipeMenu.Views.LoginPagina());
                return;
            }

            try
            {
                if (Latitud == 0 || Longitud == 0)
                {
                    await DisplayAlert("", "No hemos podido obtener su ubicación actual", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(ClienteUsuario.ClieTelefono) || ClienteUsuario.ClieTelefono.Length != 10)
                {
                    await DisplayAlert("", "Ingurese un numero de télefono", "OK");
                    return;
                }

                var confirmar = await DisplayAlert("", "¿Esta seguro de guargar la orden?", "OK", "CANCELAR");
                if (!confirmar)
                    return;

                var orden = new OrdenModelo();
                orden.OrdDescripcion = "";
                orden.OrdDireccion = "";
                orden.OrdFecha = System.DateTime.Now;
                orden.OrdFechaenvio = System.DateTime.Now;
                orden.OrdIdcliente = ClienteUsuario.ClieId;
                orden.OrdTelefono = ClienteUsuario.ClieTelefono;
                orden.OrdDescripcion = Descripcion;
                orden.OrdDireccion = Direccion;
                orden.OrdIdformapago = idmediopago;
                orden.OrdIdtienda = TiendaCarritoDetalle[0].ProdIdtienda;
                orden.OrdLatitud = Latitud;
                orden.OrdLongitud = Longitud;
                orden.OrdIdestado = 2;
                orden.OrdNumero = DataService.GenerarCodigoVerificacion();
                orden.OrdTotalcompra = TotalCompraDetalle;
                var deta = new ObservableCollection<Ordendetalle>();
                foreach (var item in TiendaCarritoDetalle)
                {
                    deta.Add(new Ordendetalle
                    {
                        DetordIdproducto = item.ProdId,
                        DetordCantidad = item.Cantidad.ToString(),
                        DetordPrecio = item.ProdPreciounitario.ToString(),
                        DetordDescuento = item.ProdDescuento.ToString(),
                    });
                }
                orden.Ordendetalles = deta;
                OrdenModelo = orden;
                var guardar = await DataService.PostGuardarAsync<OrdenModelo>(OrdenModelo, UrlModelo.odenes);
                if (guardar)
                {
                    await DisplayAlert("", "OPERACIÓN COMPLETADA \n NÚMERO DE ORDEN: " + orden.OrdNumero, "OK");
                    foreach (var item in tiendaCarritoDetalle)
                    {
                        TiendaCarrito.Remove(item);
                    }

                    TiendasCarrito.Remove(tiendaCarritoDetalle[0].ProdIdtiendaNavigation);
                    TiendaCarritoDetalle = null;
                    CalcularTotal();
                    CalcularTotalDetalle();
                    await Navigation.PopModalAsync();
                    await Navigation.PopModalAsync();
                    //await Navigation.PopModalAsync();
                    //await Navigation.PopModalAsync();
                }
            }
            catch (System.Exception ex)
            {
                await DisplayAlert("", ex.ToString(), "OK");
            }
        }
        //::::::::::::: Fin order y detalle

        //OBTENER LA UBICACION DEL CLIENTE

        private string mensajeUbicacion;

        public string MensajeUbicacion
        {
            get => mensajeUbicacion;
            set
            {
                SetProperty(ref mensajeUbicacion, value);
            }
        }
        private string colorUbicacion;

        public string ColorUbicacion
        {
            get => colorUbicacion;
            set
            {
                SetProperty(ref colorUbicacion, value);
            }
        }
        private double latitud;
        public double Latitud
        {
            get => latitud;
            set => SetProperty(ref latitud, value);
        }
        private double longitud;
        public double Longitud
        {
            get => longitud;
            set => SetProperty(ref longitud, value);
        }
        public ICommand UbicacionCommand => new Command(async () =>
        {
            IsBusy = true;
            NoIsBusy = false;

            var ubicacion = new UbicacionServicio();
            var ubi = await ubicacion.OnGetCurrentLocation();
            if (string.IsNullOrEmpty(ubi))
            {
                await Application.Current.MainPage.DisplayAlert("", "No podemos optener su ubicación \n " +
                    "verifique si esta activado el GPS", "OK");
                MensajeUbicacion = "Sin ubicacion";
                ColorUbicacion = "Red";
                IsBusy = false;
                NoIsBusy = true;
                return;
            }
            var array = ubi.Split(' ');
            Latitud = double.Parse(array[0]);
            Longitud = double.Parse(array[1]);
            ColorUbicacion = "Green";
            MensajeUbicacion = "Ubicación obtenida";
            IsBusy = false;
            NoIsBusy = true;

        });
        //::::::::::::::Fin ubicación
        void CalcularTotal()
        {
            double tot = 0;
            if (TiendaCarrito != null)
                foreach (var item in TiendaCarrito)
                {
                    tot = tot + (item.Cantidad * item.ProdPreciounitario);
                }
            TotalCompra = tot;
        }
        void CalcularTotalDetalle()
        {
            double tot = 0;
            if (TiendaCarritoDetalle != null)
                foreach (var item in TiendaCarritoDetalle)
                {
                    tot = tot + (item.Cantidad * item.ProdPreciounitario);
                }
            TotalCompraDetalle = tot;
        }
        async Task GetCategories()
        {
            IsBusy = true;
            await Task.Delay(600);
            //Categories = new ObservableCollection<Category>(DataService.GetCategories());
            if (Categorias == null)
                Categorias = await DataService.GetCategoriaAsync(UrlModelo.categoria);
            IsBusy = false;
        }
        async Task GetTienda()
        {
            IsBusy = true;
            if (Tiendas == null)
            {
                Tiendas = await DataService.GetTiendaModelosAsync();
                TiendasPremiun = new ObservableCollection<TiendaModelo>(Tiendas.Where(s => s.TienPremium == true).ToList());
            }
            IsBusy = false;
        }
        async Task GetProducts()
        {
            IsBusy = true;
            //var tienda = Tiendas.Where(s => s.selected = true);
            string urlProducto = UrlModelo.producto;
            //if (tienda != null)
            //    urlProducto = "";

            Productos = await DataService.GetProductoAsync(urlProducto);
            //Products = new ObservableCollection<Product>(DataService.GetProducts());
            IsBusy = false;
        }
        async Task ExecuteSelectCategoryCommand(TiendaModelo model)
        {
            var index = Tiendas
               .ToList()
               .FindIndex(p => p.TienRazonsocial == model.TienRazonsocial);

            if (index > -1)
            {
                UnselectGroupItems();

                Tiendas[index].selected = true;
                Tiendas[index].textColor = "#FFFFFF";
                Tiendas[index].backgroundColor = "#F4C03E";
            }
            await GetProductosTienda(model.TienId);
        }
        void UnselectGroupItems()
        {
            Tiendas.ForEach((item) =>
            {
                item.selected = false;
                item.textColor = "#000000";
                item.backgroundColor = "#EAEDF6";
            });
        }
        async Task GetProductosTienda(int id)
        {
            IsBusy = true;
            Productos = await DataService.GetProductoAsync($"{UrlModelo.producto}/tienda/{id}");
            IsBusy = false;
        }
        private async Task ExeccuteNavigateToDetailPageCommand(ProductoModelo param)
        {
            Pagina = 1;
            IsBusy = true;
            ProductoDet = param;
            await Navigation.PushModalAsync(new DetailPage { BindingContext = this });
            IsBusy = false;
        }
        private async Task ExeccuteNavigateToDetailPageCommand1(ProductoModelo param)
        {
            IsBusy = true;
            ProductoDet = param;
            await Navigation.PopModalAsync();
            await Navigation.PopModalAsync();
            //await Navigation.PopModalAsync();
            if (Pagina != 1)
                await Navigation.PushModalAsync(new DetailPage { BindingContext = this });

            //await Navigation.PushModalAsync(new DetailPage { BindingContext = this });
            IsBusy = false;
        }
        private async Task ExecutePopDetailPageCommand()
        {
            Pagina = 0;
            await Navigation.PopModalAsync();
        }

    }
}
