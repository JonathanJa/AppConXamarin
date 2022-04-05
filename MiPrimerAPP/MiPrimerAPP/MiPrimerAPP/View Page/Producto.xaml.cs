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
    public partial class Producto : ContentPage
    {
        public ProductoModel oEntityCLS { get; set; }
        //public List<ProductoCLS> listaProductos { get; set; }

       
        public string nombreproducto { get; set; }
        //sirve para realizar la busqueda
        public List<ProductoCLS> lista;
        //implementar el patron singleton
        public static Producto instance;
        public static Producto GetInstance()
        {
            if (instance == null)
            {
                return instance = new Producto();
            }
            else
            {
                return instance;
            }
        }
        public Producto()
        {
            InitializeComponent();
            instance = this;
            
            oEntityCLS = new ProductoModel();
            oEntityCLS.listaProductos = new List<ProductoCLS>();
            //oEntityCLS.listaProductos.Add(new ProductoCLS()
            //{
            //    Nombre = "Inca Cola",
            //    Precio = 8.5,
            //    Stock = 20,
            //    NombreCategoria = "Gaseosas"
            //}) ;
            //oEntityCLS.listaProductos.Add(new ProductoCLS()
            //{
            //    Nombre = "Coca Cola",
            //    Precio = 7.3,
            //    Stock = 11,
            //    NombreCategoria = "Gaseosas"
            //});
            //oEntityCLS.listaProductos.Add(new ProductoCLS()
            //{
            //    Nombre = "Soda",
            //    Precio = 1.3,
            //    Stock = 35,
            //    NombreCategoria = "Galleta"
            //});
            //oEntityCLS.listaProductos.Add(new ProductoCLS()
            //{
            //    Nombre = "Galleta de agua",
            //    Precio = 1,
            //    Stock = 8,
            //    NombreCategoria = "Gaseosas"
            //});
            
            

            BindingContext = this;
            listarProductos();
            ltsProducto.RefreshCommand = new Command(() => { listarProductos(); });
        }

        public async void listarProductos()
        {
            oEntityCLS.IsLoading = true;
            string urlGetProducto = App.Current.Resources["GetProducto"].ToString();
            lista = await GenericLH.GetAll<ProductoCLS>(urlGetProducto);
            foreach (ProductoCLS objPro in lista) objPro.ListaFotoString = ImageSource.FromFile("ic_nofoto.png");
            foreach (ProductoCLS objPro in lista.Where(p => p.foto!=null))
                objPro.ListaFotoString = GenericLH.convertirArrayDeBytesAImagenSource(objPro.foto);
            oEntityCLS.listaProductos = lista;
            oEntityCLS.IsLoading = false;
            lista = oEntityCLS.listaProductos;
            
        }

        private void searchProducto_SearchButtonPressed(object sender, EventArgs e)
        {
           SearchBar obj = sender as SearchBar;
            string texto = obj.Text;
            if (texto != "") oEntityCLS.listaProductos = lista.Where(p => p.nombre.ToUpper().Contains(texto.ToUpper())).ToList();
            else oEntityCLS.listaProductos = lista;
        }

        private void toolBarRegisProducto_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormProductos(new ProductoCLS(),"Agregar Producto"));
        }

        private async void ltsProducto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ProductoCLS objProducto = (ProductoCLS)e.SelectedItem;
            await Navigation.PushAsync(new FormProductos(objProducto, "Editar Producto"));
        }

        private async void menuEliminar_Clicked(object sender, EventArgs e)
        {
            MenuItem oMenuItem = sender as MenuItem;
            ProductoCLS oProductoCLS = (ProductoCLS)oMenuItem.BindingContext;
            int idproducto = oProductoCLS.iidproducto;
            string urlGetProducto = App.Current.Resources["GetProducto"].ToString();
            int reta = await GenericLH.Delete(urlGetProducto + "/" + idproducto);
            if (reta == 0) await DisplayAlert("Error", "No se ha podido eliminar", "cancelar");
            else
            {
                listarProductos();
                await DisplayAlert("Exito", "Se ha eliminado correctamente", "Ok");
            }
        }
    }
}