using SwipeMenu.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkingWithMaps;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
            IsBusy = true;
            Navigation = navigation;
            SelectCategoryCommand = new Command<TiendaModelo>(async (param) => await ExecuteSelectCategoryCommand(param));
            NavigateToDetailPageCommand = new Command<ProductoModelo>(async (param) => await ExeccuteNavigateToDetailPageCommand(param));
            _ = GetCategories();
            _ = GetProducts();
            _ = GetTienda();
            //IsBusy = false;
            //GetTienda();
        }

        public ICommand ShowAllCommand => new Command(async () => { await GetProducts(); });
        public ICommand CarritoCommand => new Command(async () => { await Navigation.PushModalAsync(new CarritoPage()); });
        public ICommand TiendasCommand => new Command(async () => { await Navigation.PushModalAsync(new TiendasPage { BindingContext = this }); });

        public Command NavigateToDetailPageCommand { get; }
        public Command SelectCategoryCommand { get; }
        public ObservableCollection<Category> Categories { get; set; }
        private ObservableCollection<Categoria> categorias;

        public ObservableCollection<Categoria> Categorias
        {
            get => categorias;
            set => SetProperty(ref categorias, value);
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
        private ObservableCollection<ProductoModelo> productos;

        public ObservableCollection<ProductoModelo> Productos
        {
            get => productos;
            set => SetProperty(ref productos, value);
        }
        public ICommand MapaCommand => new Command(async () =>
        {
            IsBusy = true;
            await GetTienda();
            await Navigation.PushModalAsync(new PinPage { BindingContext = this });
            IsBusy = false;
        });

        public ICommand CategoriasCommand => new Command(async () => { await GetCategories(); });

        async Task GetCategories()
        {
            IsBusy = true;
            Categories = new ObservableCollection<Category>(DataService.GetCategories());
            Categorias = await DataService.GetCategoriaAsync(UrlModelo.categoria);
            IsBusy = false;
            await Navigation.PushModalAsync(new CategoriaPage { BindingContext = this });
        }

        async Task GetTienda()
        {
            IsBusy = true;
            Tiendas = await DataService.GetTiendaModelosAsync();
            IsBusy = false;


        }

        async Task GetProducts()
        {
            IsBusy = true;
            Productos = await DataService.GetProductoAsync(UrlModelo.producto);
            Products = new ObservableCollection<Product>(DataService.GetProducts());
            IsBusy = false;
        }

        private async Task ExecuteSelectCategoryCommand(TiendaModelo model)
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
            await Navigation.PushAsync(new DetailPage(param));
            IsBusy = false;
        }
    }
}
