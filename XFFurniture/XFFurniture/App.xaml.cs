using HamburgerMenu;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFFurniture
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "Shapes_Experimental" , "Expander_Experimental" });
            //Device.SetFlags(new[] { "Expander_Experimental" });
            MainPage = new HamburgerMenu.HamburgerMenu();
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Black);
            //Title color
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
            //MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
