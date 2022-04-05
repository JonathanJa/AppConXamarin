using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using MiPrimerAPP.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class FormProductos : ContentPage
    {
        private MediaFile oMediafile;
        public ProductoModel oProductoModel { get; set; }
        public List<CategoriaCLS> listCat { get; set; }
        public List<MarcaCLS> listaMar { get; set; }
        //public ProductoCLS oProductoCLS { get; set; }
        // public List<String> listaCategoria { get; set; }
        private ProductoCLS _obj;
        public string titulo { get; set; }
        public FormProductos(ProductoCLS obj,string nombretitulo)
        {
            InitializeComponent();
            oProductoModel = new ProductoModel();
            oProductoModel.listaCategoria = new List<string>();
            oProductoModel.oProductoCLS = new ProductoCLS();

            //listaCategoria.Add("Gaseosas");
            //listaCategoria.Add("Galleta");
            //oProductoCLS = new ProductoCLS();
            //oProductoCLS.NombreCategoria = "Galleta";
            listCat = new List<CategoriaCLS>();
            listaMar = new List<MarcaCLS>();
            _obj = obj;
            titulo = nombretitulo;
            BindingContext = this;
            listarCombos();
        }

        public async void listarCombos()
        {
            oProductoModel.IsLoadingForm = true;
            string urlGetCategoriaMarca = App.Current.Resources["GetCategoriaMarca"].ToString();
            CategoriaMarcaCLS oCategoriaMarcaCLS= await GenericLH.Get<CategoriaMarcaCLS>(urlGetCategoriaMarca);
            oProductoModel.IsLoadingForm = false;
            listCat = oCategoriaMarcaCLS.listarCategoria;
            listCat.Insert(0, new CategoriaCLS { iidcategoria = 0, nombre = "--Seleccione--" });
            listaMar = oCategoriaMarcaCLS.listarMarca;
            listaMar.Insert(0, new MarcaCLS { iidmarca = 0, nombre = "--Seleccione--" });
            oProductoModel.listaCategoria = listCat.Select(p => p.nombre).ToList();
            oProductoModel.listaMarca = listaMar.Select(p => p.nombre).ToList();
           if(titulo.Equals("Agregar Producto"))
            {
                _obj.nombrecategoria = "--Seleccione--";
                _obj.nombremarca = "--Seleccione--";
            }

            oProductoModel.Image =_obj.foto == null? null :  GenericLH.convertirArrayDeBytesAImagenSource(_obj.foto);

            oProductoModel.oProductoCLS = _obj;
        }
        private async void btnAceptarProducto_Clicked(object sender, EventArgs e)
        {
            ErrorCLS oErrorCLS = GenericLH.ObligatorioTexto(limpiarProducto);
            if(oErrorCLS.exito == false)
            {
                await DisplayAlert("Error", oErrorCLS.mensaje, "Cancelar");
                return;
            }
            Producto obj = Producto.GetInstance();
            List<ProductoCLS> l = obj.oEntityCLS.listaProductos.ToList();
            
            oProductoModel.oProductoCLS.iidcategoria = listCat.Where(p => p.nombre == oProductoModel.oProductoCLS.nombrecategoria).First().iidcategoria;
            oProductoModel.oProductoCLS.iidmarca = listaMar.Where(p => p.nombre == oProductoModel.oProductoCLS.nombremarca).First().iidmarca;
            oProductoModel.IsLoadingForm = true;
            if(oMediafile != null)
            {
                oProductoModel.oProductoCLS.foto = GenericLH.convertirMediaFileAArrayDeBytes(oMediafile);
            }
            string urlGetProducto = App.Current.Resources["GetProducto"].ToString();
            int rta = await GenericLH.Post(urlGetProducto, oProductoModel.oProductoCLS);
            oProductoModel.IsLoadingForm = false;
            if (rta == 0) await DisplayAlert("error", "no se ha podido guardar en la base de datos", "ok");
            else
            {
                await Navigation.PopAsync();
                obj.listarProductos();

            }

            //if (titulo == "Agregar Produto")
            //{
            //    //agregar
            //    l.Add(oProductoModel.oProductoCLS);
            //}
            //else
            //{
            //    //editar
            //    int indixe = l.FindIndex(p => p.iidproducto == oProductoModel.oProductoCLS.iidproducto);
            //    l[indixe] = oProductoModel.oProductoCLS;
            //}


            //obj.oEntityCLS.listaProductos = l;
            await Navigation.PopAsync();
        }

        private async void btnAtrasProducto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnLimpiarProducto_Clicked(object sender, EventArgs e)
        {
            GenericLH.limpiarTexto(limpiarProducto);
        }


        private async void imagePreviewProducto_Tapped(object sender, EventArgs e)
        {
            //iniciamos la camara
            await CrossMedia.Current.Initialize();
            string opcion = await DisplayActionSheet("Elija una opcion", "Cancelar", null, "Galeria", "Camara");
            switch (opcion)
            {
                case "Galeria":

                    oMediafile = await CrossMedia.Current.PickPhotoAsync();

                    break;
                case "Camara":
                    await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Pictures",
                        Name = "Prueba.jpg",
                        PhotoSize = PhotoSize.Small
                    });
                    ; break;
                default:
                    oMediafile = null
                    ; break;

            }
            if (oMediafile != null) { oProductoModel.Image = GenericLH.convertirMediaFileAImageSource(oMediafile); }

        }
    }
}