

using SwipeMenu.Views;
using System.Collections.Generic;
using WorkingWithMaps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture;
using XFFurniture.Models;
using XFFurniture.Service;
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

            MyMenu();

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
        }

        public void MyMenu()
        {
            var contexto = new MainPageViewModel(Navigation);
            Detail = new NavigationPage(new MainPage { BindingContext = contexto });
            BindingContext = contexto;
            //ListMenu.ItemsSource = MainPageViewModel.MenuApp;
            //List<MenuApp> menu = new List<MenuApp>
            //{
            //    new MenuApp{ Page= new TiendasPage(),MenuTitle="Tiendas",  MenuDetail="Mi perfil",icon="user.png"},
            //    new MenuApp{ Page= new PinPage(),MenuTitle="Mapa",  MenuDetail="Mensajes",icon="message.png"},
            //    new MenuApp{ Page= new CategoriaPage(),MenuTitle="Categorias",  MenuDetail="Contactos",icon="contacts.png"},
            //    new MenuApp{ Page= new MainPage(),MenuTitle="Mis pedidos",  MenuDetail="Configuración",icon="settings.png"}
            //};
            //ListMenu.ItemsSource = menu;
        }

        MainPageViewModel MainPageViewModel => ((MainPageViewModel)Detail.BindingContext);
        private async void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as MenuApp;
            if (menu != null)
            {
                if (menu.MenuTitle == "Perfil")
                {
                    if (!await UsuarioServicio.EstadologinAsync())
                    {
                        //Detail = new NavigationPage(new LoginPagina());
                        await Navigation.PushModalAsync(new LoginPagina());
                        IsPresented = false;
                        return;
                    }
                    else
                    {
                        await Navigation.PushModalAsync(new MisDatosPage());
                        IsPresented = false;
                        return;
                    }
                }

                Detail = new NavigationPage(menu.Page);
                IsPresented = false;
                //Detail = new NavigationPage(new CategoriaPage { BindingContext=MainPageViewModel });
                //MainPageViewModel.OpcionMenuCommand.Execute(menu.Page);

                //Detail.Navigation.PopModalAsync(menu.Page);
                //await Navigation.PushAsync(new menu.Page);
            }
        }

    }
}