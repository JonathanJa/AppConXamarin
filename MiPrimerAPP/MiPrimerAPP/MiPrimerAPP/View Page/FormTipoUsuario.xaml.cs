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
    public partial class FormTipoUsuario : ContentPage
    {
        public TipoUsuarioModel oEntityCLS { get; set; }
        public string titulo { get; set; }
        private List<PaginaCLS> _ListarPaginas;
        public FormTipoUsuario(int iidTipoUsuario)
        {
            InitializeComponent();
            oEntityCLS = new TipoUsuarioModel();
            oEntityCLS.oTipoUsuarioCLS = new TipoUsuarioCLS();
            if(iidTipoUsuario == 0)
            {
                titulo = "Agregar TipoUsuario";
            }
            else
            {
                titulo = "Editar TipoUsuario";
            }
            BindingContext = this;
            listarDatos(iidTipoUsuario);
        }

        public async void listarDatos( int iidtipousuario)
        {
            string GetPagina = App.Current.Resources["GetPagina"].ToString();
            string GetTipoUsuario = App.Current.Resources["GetTipoUsuario"].ToString();
            if (titulo.Equals("Agregar TipoUsuario"))
            {
                _ListarPaginas = await GenericLH.GetAll<PaginaCLS>(GetPagina);
            }
            else
            {
                TipoUsuarioCLS oTipoUsuarioCLS = await GenericLH.Get<TipoUsuarioCLS>(GetTipoUsuario + "/" + iidtipousuario);
                oEntityCLS.oTipoUsuarioCLS = oTipoUsuarioCLS;
                _ListarPaginas = oTipoUsuarioCLS.listaPagina;
            }
            oEntityCLS.listaPagina = _ListarPaginas;
        }

        private async void toolBarAgregar_Clicked(object sender, EventArgs e)
        {
            TipoUsuario obj = TipoUsuario.GetInstance();
            string getTipoUsuario = App.Current.Resources["GetTipoUsuario"].ToString();
           List<PaginaCLS> pag = oEntityCLS.listaPagina;
            List<int> PaginasMarcada = pag.Where(p => p.bseleccionado == true).Select(p => p.iidpagina).ToList();
            //List<string> nombrePaginaMarcadas = pag.Where(p => p.bseleccionado == true).Select(p => p.nombre).ToList();
            oEntityCLS.oTipoUsuarioCLS.iidtipousuarioList = PaginasMarcada;
            int rta = await GenericLH.Post<TipoUsuarioCLS>(getTipoUsuario, oEntityCLS.oTipoUsuarioCLS);
            if (rta == 1) 
            { 
                obj.listaTipoUsuario();
                await Navigation.PopAsync();
            }
        }
    }
}