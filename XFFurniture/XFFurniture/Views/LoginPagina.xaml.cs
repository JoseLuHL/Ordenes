using SwipeMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwipeMenu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPagina : ContentPage
    {
 
        public LoginPagina()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
            //UsernameEntry.Completed += (sender, args) => { PasswordEntry.Focus(); };
            //PasswordEntry.Completed += (sender, args) => { ViewModel.AuthenticateCommand.Execute(null); };
        }

    }
}