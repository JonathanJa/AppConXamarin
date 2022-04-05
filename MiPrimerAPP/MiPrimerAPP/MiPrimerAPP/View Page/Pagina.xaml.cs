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
    public partial class Pagina : ContentPage
    {
        public PaginaModel oEntityCLS { get; set; }
        private List<PaginaCLS> lista;
        public static Pagina instance;
        public static Pagina GetInstance()
        {
            if (instance == null)
            {
                return instance = new Pagina();
            }
            else
            {
                return instance;
            }
        }
        public Pagina()
        {
            instance = this;
            oEntityCLS = new PaginaModel();
            InitializeComponent();
            BindingContext = this;
            listaPagina();
            ltsPagina.RefreshCommand = new Command(() =>
            {
                listaPagina();
            });
        }

        public async void listaPagina()
        {
            oEntityCLS.IsLoading = true;
            string urlGetpagina = App.Current.Resources["GetPagina"].ToString();
            List<PaginaCLS> Pagina  = await GenericLH.GetAll<PaginaCLS>(urlGetpagina);
            oEntityCLS.IsLoading = false;
            oEntityCLS.listaPagina =Pagina;
            
            lista = oEntityCLS.listaPagina;
        }

        private async void ltsPagina_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PaginaCLS objPagina = (PaginaCLS)e.SelectedItem;
            await Navigation.PushAsync(new FormPagina(objPagina, "Editar Categoria"));
        }

        private void menuEliminar_Clicked(object sender, EventArgs e)
        {

        }

        private async void toolBarRegis_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormPagina(new PaginaCLS(), "Agregar Pagina"));
        }
    }
}