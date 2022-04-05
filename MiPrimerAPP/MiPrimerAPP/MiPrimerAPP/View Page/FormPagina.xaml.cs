using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using MiPrimerAPP.Model;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiPrimerAPP.View_Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPagina : ContentPage
    {
        public PaginaCLS oPaginaCLS { get; set; } = new PaginaCLS();
        public string titulo { get; set; }
        public FormPagina(PaginaCLS obj, string nombretitulo)
        {
            oPaginaCLS = obj;
            titulo = nombretitulo;
            InitializeComponent();
            BindingContext = this;
        }

        private async void btnAceptar_Clicked(object sender, EventArgs e)
        {
            Pagina obj = Pagina.GetInstance();
            List<PaginaCLS> P = obj.oEntityCLS.listaPagina.ToList();
            string urlGetPagina = App.Current.Resources["GetPagina"].ToString();
            ErrorCLS oError = GenericLH.ObligatorioTexto(limpiarPagina);
            if (oError.exito == false)
            {
                await DisplayAlert("Alerta", oError.mensaje, "cancelar");
                return;
            }
            
            var rpta = await GenericLH.Post(urlGetPagina, oPaginaCLS);
            if (rpta != 0)
            {
                await Navigation.PopAsync();
                obj.listaPagina();

            }

        }

        private async void btnAtras_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnLimpiar_Clicked(object sender, EventArgs e)
        {
            GenericLH.limpiarTexto(limpiarPagina);
        }
    }
}