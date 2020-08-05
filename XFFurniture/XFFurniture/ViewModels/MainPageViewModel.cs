using SwipeMenu.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingWithMaps;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XFFurniture.Interfaces;
using XFFurniture.Models;
using XFFurniture.Service;
using XFFurniture.ViewModel;
using XFFurniture.Views;

namespace XFFurniture.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation)
        {
            IsLoad = true;
            IsCargando = false;
            IsBusy = true;
            Navigation = navigation;
            DependencyService.Get<IStatusBarStyle>().ChangeTextColor(true);
            PopDetailPageCommand = new Command(async () => await ExecutePopDetailPageCommand());
            SelectCategoryCommand = new Command<TiendaModelo>(async (param) => await ExecuteSelectCategoryCommand(param));
            NavigateToDetailPageCommand = new Command<ProductoModelo>(async (param) => await ExeccuteNavigateToDetailPageCommand(param));
            //_ = GetCategories();
            _ = GetTienda();
            _ = GetProducts();
            //IsBusy = false;
            //GetTienda();
            //IsCargando = false;
            //IsLoad = true;
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
            await Navigation.PushModalAsync(new CarritoPage { BindingContext = this });
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
                //    await DisplayAlert("", "El producto no se ha agregado", "OK");

                if (CarritoAn != null)
                {
                    foreach (var item in CarritoAn)
                    {
                        tiendaCarrito_.Add(item);
                    }
                }
                ProductoDet.Cantidad++;
                TiendaCarrito = tiendaCarrito_;

                TotalCarrito = TiendaCarrito.Count;
                //totalCarrito = TiendaCarrito.Count;
                //ProductoDet = null;
                //if (ProductoDet.Cantidad)
                //{

                //}
                //await DisplayAlert("", "PRODUCTO AGREGADO", "OK");

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
                            ProductoDet.Cantidad=0;
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

        private int totalCarrito;
        public int TotalCarrito
        {
            get => totalCarrito;
            set
            {
                SetProperty(ref totalCarrito, value);
            }
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
            Products = new ObservableCollection<Product>(DataService.GetProducts());
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
            IsBusy = true;
            ProductoDet = param;
            await Navigation.PushAsync(new DetailPage { BindingContext = this });
            IsBusy = false;
        }
        private async Task ExecutePopDetailPageCommand()
        {
            await Navigation.PopAsync();
        }

    }
}
