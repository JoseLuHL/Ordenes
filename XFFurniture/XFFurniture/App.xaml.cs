using HamburgerMenu;
using SwipeMenu.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSQLite;
using XFFurniture.ViewModels;

namespace XFFurniture
{
    public partial class App : Application
    {
        static SQLiteHelper db;

        public static SQLiteHelper SQLiteDb
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinSQLite.db3"));
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "Shapes_Experimental" , "Expander_Experimental" });
            //Device.SetFlags(new[] { "Expander_Experimental" });
            //MainPage = new HamburgerMenu.HamburgerMenu();            
            MainPage = new NavigationPage(new LoginPagina());
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
