using System;
using System.Collections.ObjectModel;
using WorkingWithMaps.Modelo;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XFFurniture.Models;
using XFFurniture.ViewModels;

namespace WorkingWithMaps
{
    public partial class PinPage : ContentPage
    {
        //List<Pin> boardwalkPin;
        MainPageViewModel tiendas => ((MainPageViewModel)BindingContext);
        Property property;
        public PinPage()
        {
            InitializeComponent();

            //Pin boardwalkPin = new Pin
            //{
            //    Position = new Position(5.687629, -76.659698),
            //    Label = "Chocó",
            //    Address = "Quibdo",
            //    Type = PinType.Place
            //};

            //map.Pins.Add(boardwalkPin);

        }

        //public PinPage(Property property)
        //{
        //    InitializeComponent();
        //    //contexto.PropertyTypeList
        //    map.IsShowingUser = true;
        //    this.property = property;

        //}

        protected override void OnAppearing()
        {
            //if (property != null)
            //{
            //    Pin listPin = new Pin
            //    {
            //        Position = new Position(property.Lat, property.Lng),
            //        AutomationId = property.Id,
            //        Label = property.Price,
            //        Address = property.Space,
            //        Type = PinType.Place
            //    };
            //    map.Pins.Add(listPin);

            //}
            //else

            Cargar();

            base.OnAppearing();
        }

        public void Cargar()
        {
            map.IsShowingUser = true;
            Pin boardwalkPin = new Pin();
            Pin listPin = new Pin();
            if (tiendas.Tiendas.Count > 0)
                foreach (var item in tiendas.Tiendas)
                {
                    if (item.TienLatitud != 0 && item.TienLongitud != 0)
                    {
                        listPin = new Pin
                        {
                            Position = new Position(item.TienLatitud, item.TienLongitud),
                            BindingContext = item,
                            AutomationId = item.TienId.ToString(),
                            Label = item.TienRazonsocial,
                            Address = item.TienDireccion,
                            Type = PinType.Place
                        };

                        //listPin.MarkerClicked += OnMarkerClickedAsync;
                        listPin.InfoWindowClicked += OnInfoWindowClickedAsync;
                        map.Pins.Add(listPin);
                    }
                }


            //boardwalkPin.MarkerClicked += OnMarkerClickedAsync;


            //Pin wharfPin = new Pin
            //{
            //    Position = new Position(5.6845709, -76.6540463),
            //    Label = "Chocó",
            //    Address = "Quibdo",
            //    Type = PinType.Place
            //};

            //wharfPin = new Pin
            //{
            //    Position = new Position(5.68628372017091, -76.66052389815435),
            //    Label = "Chocó",
            //    Address = "Quibdo",
            //    Type = PinType.Place
            //};
            //wharfPin.InfoWindowClicked += OnInfoWindowClickedAsync;


            //map.Pins.Add(wharfPin);
        }
        void OnButtonClicked(object sender, EventArgs e)
        {
            Cargar();
        }

        async void OnMarkerClickedAsync(object sender, PinClickedEventArgs e)
        {
            e.HideInfoWindow = true;
            string pinName = ((Pin)sender).Label;
            await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
        }

        async void OnInfoWindowClickedAsync(object sender, PinClickedEventArgs e)
        {

            var property = ((Pin)sender).BindingContext as TiendaModelo;
            string pinName = ((Pin)sender).Label;
            tiendas.SelectCategoryCommand.Execute(property);
            //await DisplayAlert("", property.Price , "Ok");
            //await Navigation.PushModalAsync(new DetailsPage(property));
        }
    }
}
