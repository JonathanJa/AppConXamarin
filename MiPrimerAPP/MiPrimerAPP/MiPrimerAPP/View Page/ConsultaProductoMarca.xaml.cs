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
    public partial class ConsultaProductoMarca : ContentPage
    {
        public ConsultaProductoMarcaModel oConsultaProductoMarcaModel { get; set; }
        private List<MarcaCLS> listamarca;
        private List<ProductoCLS> listaproducto;
        public ConsultaProductoMarca()
        {
            InitializeComponent();
            oConsultaProductoMarcaModel = new ConsultaProductoMarcaModel();
            BindingContext = this;
            listaDatos();

        }
        public async void listaDatos()
        {
            string urlGetMarca = App.Current.Resources["GetMarca"].ToString();
            listamarca = await GenericLH.GetAll<MarcaCLS>(urlGetMarca);
            listamarca.Insert(0, new MarcaCLS { iidmarca = 0, nombre = "Todos" });
            oConsultaProductoMarcaModel.listaMarca = listamarca.Select(p => p.nombre).ToList();
            string urlGetProducto = App.Current.Resources["GetProducto"].ToString();
            listaproducto = await GenericLH.GetAll<ProductoCLS>(urlGetProducto);
            oConsultaProductoMarcaModel.listaProductos = listaproducto;
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            string buscar = oConsultaProductoMarcaModel.NombreMarca;
            if (buscar != "Todos")
                oConsultaProductoMarcaModel.listaProductos = listaproducto.Where(p => p.nombremarca == buscar).ToList();
            else
                oConsultaProductoMarcaModel.listaProductos = listaproducto;
        }
    }
}