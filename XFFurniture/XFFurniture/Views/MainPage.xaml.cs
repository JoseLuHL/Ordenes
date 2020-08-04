﻿using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFFurniture.Interfaces;
using XFFurniture.ViewModels;

namespace XFFurniture
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel MainPageViewModel => ((MainPageViewModel)BindingContext);
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
            
        }

        protected async override void OnAppearing()
        {
            MainPageViewModel.IsLoad = true;
            MainPageViewModel.IsCargando = false;

            await Task.Delay(700);
            DependencyService.Get<IStatusBarStyle>().ChangeTextColor();

            MainPageViewModel.IsLoad = false;
            MainPageViewModel.IsCargando = true;
        }
    }
}
