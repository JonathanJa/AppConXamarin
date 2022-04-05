using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using MiPrimerAPP.Model;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;
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
    public partial class Pedido : ContentPage
    {
        public PedidoModel oEntityCLS { get; set; }
        private List<DetalleVentaCLS> lista;
        private decimal total;
        public Pedido()
        {
            InitializeComponent();
            oEntityCLS = new PedidoModel();
            oEntityCLS.oVentasCLS = new VentasCLS();
            if(Settings.ProductListAdd != "")
            {
                lista = JsonConvert.DeserializeObject<List<DetalleVentaCLS>>(Settings.ProductListAdd);
            }
            else
            {
                lista = new List<DetalleVentaCLS>();
            }
            total = lista.Select(p => p.precio * p.cantidad).Sum();
            oEntityCLS.oVentasCLS = new VentasCLS() { total = total};

            oEntityCLS.listaProductos = lista;
            BindingContext = this;

        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            ImageButton btnBot = sender as ImageButton;
            DetalleVentaCLS oProducto = (DetalleVentaCLS)btnBot.BindingContext;
            int iidProducto = oProducto.iidproducto;
            if (Settings.ProductListAdd != "")
            {
                List<DetalleVentaCLS> listapro = JsonConvert.DeserializeObject<List<DetalleVentaCLS>>(Settings.ProductListAdd);
                List<DetalleVentaCLS> listanew = listapro.Where(p => p.iidproducto != iidProducto).ToList();
                oEntityCLS.listaProductos = listanew;
                Settings.ProductListAdd = JsonConvert.SerializeObject(listanew);
                total = listanew.Select(p => p.precio * p.cantidad).Sum();
                oEntityCLS.oVentasCLS = new VentasCLS() { total = total };
            }
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            
            
           string opcion = await DisplayActionSheet("Accion", "Cancelar", null, "Si", "No");
            if (opcion == "no") return;
            CompraCLS oCompraCLS = new CompraCLS();
            oCompraCLS.oVentasCLS = new VentasCLS();
            oCompraCLS.oVentasCLS.iidusuario = Settings.idUsuario;
            oCompraCLS.oVentasCLS.total = total;
            oCompraCLS.listadetalles = oEntityCLS.listaProductos;
            string getVenta = App.Current.Resources["GetVenta"].ToString();
            int rta = await GenericLH.Post<CompraCLS>(getVenta, oCompraCLS);
            if(rta == 1)
            {
                Settings.ProductListAdd = "";
                oEntityCLS.listaProductos = new List<DetalleVentaCLS>();
                await DisplayAlert("Alerta", "Se ha guardado correctamente", "OK");
                oEntityCLS.oVentasCLS = new VentasCLS() { total = 0 };
            }
            else
            {
                await DisplayAlert("Alerta", "ha ocurrido un error", "Cancel");
            }
        }
    }
}