using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFFurniture.Models;
using XFFurniture.ViewModels;

namespace XFFurniture.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriaPage : ContentPage
    {
        MainPageViewModel MainPageViewModel => ((MainPageViewModel)BindingContext);
        public CategoriaPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            var subcate = new ObservableCollection<Subcategorium>();
            foreach (var item in MainPageViewModel.Categorias)
            {
                foreach (var item2 in item.Subcategoria)
                {
                    if (item.Todos)
                    {
                        item2.Activo = true;
                        subcate.Add(item2);
                    }
                    else
                        subcate.Add(new Subcategorium
                        {
                            SubcatId = item2.SubcatId,
                            SubcatDescripcion = item2.SubcatDescripcion,
                            SubcatFoto = item2.SubcatFoto
                        });
                }
            }

            MainPageViewModel.SubCategorias = subcate;
            var g = MainPageViewModel.SubCategorias;
            return base.OnBackButtonPressed();
        }
    }
}