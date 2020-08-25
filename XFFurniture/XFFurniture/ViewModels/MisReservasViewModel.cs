using SwipeMenu.Models;
using SwipeMenu.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFFurniture.Models;
using XFFurniture.Service;
using XFFurniture.ViewModel;
using XFFurniture.Views.Ordenes;

namespace XFFurniture.ViewModels
{
    public class MisReservasViewModel : BaseViewModel
    {
        public MisReservasViewModel(INavigation navigation)
        {
            try
            {
                IsBusy = true;
                NoIsBusy = false;
                Navigation = navigation;
                _ = LoadAsync();
            }
            catch (Exception es)
            {

                _ = DisplayAlert("Mis res", es.ToString(), "OK");
            }
           

            //_ = MisOrdenes();
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

        private ClienteModelo clienteModelo;

        public ClienteModelo ClienteModelo
        {
            get => clienteModelo;
            set
            {
                SetProperty(ref clienteModelo, value);
            }
        }

        private ObservableCollection<OrdenModelo> ordenModelos;

        public ObservableCollection<OrdenModelo> OrdenModelos
        {
            get => ordenModelos;
            set
            {
                SetProperty(ref ordenModelos, value);
            }
        }
        private OrdenModelo ordenModelosSelect;

        public OrdenModelo OrdenModelosSelect
        {
            get => ordenModelosSelect;
            set
            {
                SetProperty(ref ordenModelosSelect, value);
            }
        }

        private bool estadoCancelar;

        public bool EstadoCancelar
        {
            get => estadoCancelar;
            set
            {
                SetProperty(ref estadoCancelar, value);
            }
        }


        public async Task LoadAsync()
        {
            if (await UsuarioServicio.EstadologinAsync())
            {
                var dat = await App.SQLiteDb.GetItemsAsync();
                UsuarioServicio.Cliente = dat[0];
                ClienteModelo = dat[0];
                await MisOrdenes(dat[0].ClieId.ToString());
                //Id = dat[0].ClieId.ToString();
            }
            else
            {
               //await Navigation.PopModalAsync();
                await Navigation.PushModalAsync(new LoginPagina());
            }
            IsBusy = false;
            NoIsBusy = true;
        }
        private string id;

        public string Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
            }
        }

        public async Task MisOrdenes(string id)
        {
            IsBusy = true;
            NoIsBusy = false;
            IsBusy = true;
            var idC = id;
            OrdenModelos = await DataService.GetOrdenModelosAsync($"{UrlModelo.odenes}/cliente/{id}");
            IsBusy = false;
            NoIsBusy = true;

            if (OrdenModelos.Count <= 0)
            {
                //await DisplayAlert("", "Aun no hay ordenes", "OK");
                await Navigation.PopModalAsync();
            }
        }

        public ICommand TiendasCarritoCommand => new Command<OrdenModelo>(execute: async (orden) =>
        {
            OrdenModelosSelect = orden;
            if (orden.OrdIdestadoNavigation != null
            && orden.OrdIdestadoNavigation.EsorIdDescripcion == "Pendiente")
                EstadoCancelar = true;
            else
                EstadoCancelar = false;
            await Navigation.PushModalAsync(new MisOrdenesDetalle { BindingContext = this });
        });

        public ICommand AbandonarCommand => new Command(execute: async () =>
       {
           await Abandonar();
           IsBusy = false;
           NoIsBusy = true;
       });

        private async Task Abandonar()
        {
            var res = await DisplayAlert("", "¿Esta seguro de cancelar la order?", "OK", "CANCELAR");
            if (res)
            {
                IsBusy = true;
                NoIsBusy = false;
                if (ordenModelosSelect.OrdIdestadoNavigation.EsorIdDescripcion == "Pendiente")
                {
                    var datos = await DataService.GetAsync<OrdenModelo>($"{UrlModelo.odenes}/{OrdenModelosSelect.OrdId}");
                    
                    if (datos.OrdIdestadoNavigation.EsorIdDescripcion== "Pendiente")
                    {
                        OrdenModelosSelect.OrdIdestado = 3;
                        var act = await DataService.PutActualizarAsync<OrdenModelo>(OrdenModelosSelect, $"{UrlModelo.odenes}/actualizar/{ordenModelosSelect.OrdId}");
                        await Navigation.PopModalAsync();
                        await LoadAsync();
                    }
                }
            }

        }

        //async Task ExecuteSelectCategoryCommand(TiendaModelo model)
        //{
        //    var index = Tiendas
        //       .ToList()
        //       .FindIndex(p => p.TienRazonsocial == model.TienRazonsocial);

        //    if (index > -1)
        //    {
        //        UnselectGroupItems();

        //        Tiendas[index].selected = true;
        //        Tiendas[index].textColor = "#FFFFFF";
        //        Tiendas[index].backgroundColor = "#F4C03E";
        //    }
        //    await GetProductosTienda(model.TienId);
        //}
        //void UnselectGroupItems()
        //{
        //    Tiendas.ForEach((item) =>
        //    {
        //        item.selected = false;
        //        item.textColor = "#000000";
        //        item.backgroundColor = "#EAEDF6";
        //    });
        //}
    }
}
