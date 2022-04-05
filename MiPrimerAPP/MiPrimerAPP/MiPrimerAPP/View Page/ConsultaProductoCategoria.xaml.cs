using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using MiPrimerAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiPrimerAPP.View_Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaProductoCategoria : ContentPage
    {
        public ConsultaProductoCategoriaModel oConsultaProductoCategoriaModel { get; set; }
        private List<CategoriaCLS> listacategoria;
        private List<ProductoCLS> listaproducto;
        public ConsultaProductoCategoria()
        {
            InitializeComponent();
            oConsultaProductoCategoriaModel = new ConsultaProductoCategoriaModel();
            oConsultaProductoCategoriaModel.OCategoriaModel = new CategoriaModel();
            BindingContext = this;
            listaDatos();
            ltsProducto.Command = new Command(() =>
            {
                listaDatos();
            });


        }

        public async void listaDatos()
        {
            oConsultaProductoCategoriaModel.OCategoriaModel.IsLoading = true;
            string urlGetCategoria = App.Current.Resources["GetCategoria"].ToString();
            listacategoria = await GenericLH.GetAll<CategoriaCLS>(urlGetCategoria);
            listacategoria.Insert(0, new CategoriaCLS { iidcategoria = 0, nombre = "Todos" });
            oConsultaProductoCategoriaModel.listaCategoria = listacategoria.Select(p => p.nombre).ToList();
            string urlGetProducto = App.Current.Resources["GetProducto"].ToString();
            listaproducto = await GenericLH.GetAll<ProductoCLS>(urlGetProducto);
            oConsultaProductoCategoriaModel.listaProductos =listaproducto;
            oConsultaProductoCategoriaModel.OCategoriaModel.IsLoading = false;
        }

        private void pickerCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Picker p = sender as Picker;
            //string item = p.SelectedItem.ToString();
            string item = oConsultaProductoCategoriaModel.NombreCategoria;
            if (item != "Todos")
                oConsultaProductoCategoriaModel.listaProductos = listaproducto.Where(p => p.nombrecategoria == item).ToList();
            else
                oConsultaProductoCategoriaModel.listaProductos = listaproducto;
        }
    }
}