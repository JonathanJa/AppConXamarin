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
    public partial class TipoUsuario : ContentPage
    {
        public TipoUsuarioModel oEntityCLS { get; set; }
        public static TipoUsuario instance;
        public static TipoUsuario GetInstance()
        {
            if (instance == null)
            {
                return instance = new TipoUsuario();
            }
            else
            {
                return instance;
            }
        }
        public TipoUsuario()
        {
            InitializeComponent();
            instance = this;
            oEntityCLS = new TipoUsuarioModel();
            
            BindingContext = this;
            listaTipoUsuario();
        }

        public async void listaTipoUsuario()
        {
            string getTipoUsuario = App.Current.Resources["GetTipoUsuario"].ToString();
            oEntityCLS.listaTipoUsuario = await GenericLH.GetAll<TipoUsuarioCLS>(getTipoUsuario);
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            ImageButton boton = sender as ImageButton;
            TipoUsuarioCLS obj = (TipoUsuarioCLS)boton.BindingContext;
            int iidTipoUsuario = obj.iidtipousuario;
            await Navigation.PushAsync(new FormTipoUsuario(iidTipoUsuario));
        }

        private async void toolBarRegis_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormTipoUsuario(0));
        }
    }
}