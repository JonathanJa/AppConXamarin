using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using MiPrimerAPP.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiPrimerAPP.View_Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categoria : ContentPage
    {
        public CategoriaModel oEntityCLS { get; set; }
        //public List<CategoriaCLS> listaCategoria { get; set; }
        private List<CategoriaCLS> lista;
        //implementar el patron singleton
        public static Categoria instance;
        public static Categoria GetInstance()
        {
            if(instance == null)
            {
                return instance = new Categoria();
            }
            else
            {
                return instance;
            }
        }
        public Categoria()
        {
            InitializeComponent();
            instance = this;
            oEntityCLS = new CategoriaModel();
            oEntityCLS.listaCategoria = new List<CategoriaCLS>();
            //oEntityCLS.listaCategoria.Add(new CategoriaCLS()
            //{
            //    Iidcategoria = 1,
            //    Nombre = "Gaseosas",
            //    Descripcion = "bebida hecha a base de agua carbonatada, edulcorantes naturales como fructosa o sacarosa o sintéticos como el ciclamato (E952), " +
            //    "acidulantes, colorantes, antioxidantes, " +
            //    "estabilizadores de acidez y conservadores."
            //}) ;

            //oEntityCLS.listaCategoria.Add(new CategoriaCLS()
            //{
            //    Iidcategoria = 2,
            //    Nombre = "Galleta",
            //    Descripcion = "preparación culinaria de pequeño tamaño, dulce o salada, horneada y hecha normalmente a base de harina de trigo, huevos, azúcar, " +
            //    "mantequilla o aceites vegetales o grasas animales."
            //});
            
            BindingContext = this;
            listarCategoria();
            ltsCategoria.RefreshCommand = new Command(() =>
            {
                listarCategoria();
            });
            
        }

        private async void ltsCategoria_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CategoriaCLS objCategoria = (CategoriaCLS)e.SelectedItem;
            await Navigation.PushAsync(new FormCategoria(objCategoria,"Editar Categoria"));
        }

        private async void toolBarRegis_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new FormCategoria(new CategoriaCLS(),"Agregar Categoria"));
        }

        private async void menuEliminar_Clicked(object sender, EventArgs e)
        {
            MenuItem oMenuItem = sender as MenuItem;
            CategoriaCLS oCategoriaCLS = (CategoriaCLS)oMenuItem.BindingContext;
            string urlGetCategoria = App.Current.Resources["GetCategoria"].ToString();
            int idcategoria = oCategoriaCLS.iidcategoria;
            int reta = await GenericLH.Delete(urlGetCategoria + "/" + idcategoria);
            if(reta==0) await DisplayAlert("Error", "No se ha podido eliminar", "cancelar");
            else
            {
                listarCategoria();
                await DisplayAlert("Exito", "Se ha eliminado correctamente", "Ok");
            }

            //HttpClient cliente = new HttpClient();
            //var rta = await cliente.DeleteAsync("http://juan21-001-site1.ctempurl.com/api/categoria/" + idcategoria);
            //if (!rta.IsSuccessStatusCode) await DisplayAlert("Error", "No se ha podido eliminar", "cancelar");
            //else
            //{
            //    var result = await rta.Content.ReadAsStringAsync();
            //    if (result == "0") await DisplayAlert("Error", "Ha ocurrido un error en la base de datos", "cancel");
            //    else listarCategoria();


            //}

            //oEntityCLS.listaCategoria = oEntityCLS.listaCategoria.Where(p => p.Iidcategoria != oCategoriaCLS.Iidcategoria).ToList();
            //DisplayAlert("Alerta", oCategoriaCLS.Nombre, "cancelar");
        }

        private void searchCategoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            string valor = e.NewTextValue;
            if (valor == "") oEntityCLS.listaCategoria = lista;
            else oEntityCLS.listaCategoria = lista.Where(p => p.nombre.ToUpper().Contains(valor.ToUpper())).ToList();
            //DisplayAlert("alerta", valor, "cancelar");
        }


        public async void listarCategoria()
        {
            oEntityCLS.IsLoading = true;
            string urlGetCategoria = App.Current.Resources["GetCategoria"].ToString();
            List<CategoriaCLS> l = await GenericLH.GetAll<CategoriaCLS>(urlGetCategoria);
            oEntityCLS.IsLoading = false;
            oEntityCLS.listaCategoria = l;
            foreach (CategoriaCLS obj in l) obj.ListaFotoString = ImageSource.FromFile("ic_nofoto.png");
            foreach (CategoriaCLS obj in l.Where(p => p.foto != "")) obj.ListaFotoString = GenericLH.convertirBase64aImageSource(obj.foto);
            lista = oEntityCLS.listaCategoria;
            // HttpClient cliente = new HttpClient();
            //var rta = await cliente.GetAsync("http://juan21-001-site1.ctempurl.com/api/categoria");
            // if (!rta.IsSuccessStatusCode) oEntityCLS.listaCategoria = new List<CategoriaCLS>();
            // else
            // {
            //     var result = await rta.Content.ReadAsStringAsync();
            //     List<CategoriaCLS> l = JsonConvert.DeserializeObject<List<CategoriaCLS>>(result);
            //     oEntityCLS.listaCategoria = l;
            // }
        }
    }
}