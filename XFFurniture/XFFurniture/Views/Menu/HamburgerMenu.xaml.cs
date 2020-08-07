

using System.Collections.Generic;
using WorkingWithMaps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture;
using XFFurniture.ViewModels;
using XFFurniture.Views;

namespace HamburgerMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HamburgerMenu : MasterDetailPage
    {
        public HamburgerMenu()
        {
            InitializeComponent();
            //BindingContext = new MainPageViewModel(Navigation);
            MyMenu();
        }

        protected override void OnAppearing()
        {
           
            base.OnAppearing(); 
        }

        public void MyMenu()
        {
            Detail = new NavigationPage(new MainPage ());
            List<Menu> menu = new List<Menu>
            {
                new Menu{ Page= new TiendasPage(),MenuTitle="Tiendas",  MenuDetail="Mi perfil",icon="user.png"},
                new Menu{ Page= new PinPage(),MenuTitle="Mapa",  MenuDetail="Mensajes",icon="message.png"},
                new Menu{ Page= new CategoriaPage(),MenuTitle="Categorias",  MenuDetail="Contactos",icon="contacts.png"},
                new Menu{ Page= new MainPage(),MenuTitle="Mis pedidos",  MenuDetail="Configuración",icon="settings.png"}
            };
            ListMenu.ItemsSource = menu;
        }

        MainPageViewModel MainPageViewModel => ((MainPageViewModel)Detail.BindingContext);
        private async void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;
            if (menu != null)
            {
                IsPresented = false;
                //Detail = new NavigationPage(menu.Page );
                //Detail = new NavigationPage(new CategoriaPage { BindingContext=MainPageViewModel });
                MainPageViewModel.CategoriasCommand.Execute(null);

                //Detail.Navigation.PopModalAsync(new TiendasPage());
                //await Navigation.PushAsync(new menu.Page);
            }
        }
        public class Menu
        {
            public string MenuTitle
            {
                get;
                set;
            }
            public string MenuDetail
            {
                get;
                set;
            }

            public ImageSource icon
            {
                get;
                set;
            }

            public Page Page
            {
                get;
                set;
            }
        }
    }
}