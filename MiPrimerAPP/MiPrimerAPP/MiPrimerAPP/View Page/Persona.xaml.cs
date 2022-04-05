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
    public partial class Persona : ContentPage
    {
        public static Persona instance;
        public static Persona GetInstance()
        {
            if (instance == null)
            {
                return instance = new Persona();
            }
            else
            {
                return instance;
            }
        }
        public PersonaModel oEntityCLS { get; set; }
        //public List<CategoriaCLS> listaCategoria { get; set; }
        private List<PersonaCLS> lista;
        public Persona()
        {
            InitializeComponent();
            instance = this;
            oEntityCLS = new PersonaModel();
            oEntityCLS.listaPersona = new List<PersonaCLS>();
            BindingContext = this;
            listaPersonas();
        }

        public async void listaPersonas()
        {
            string urlGetPersona = App.Current.Resources["GetPersona"].ToString();
            lista = await GenericLH.GetAll<PersonaCLS>(urlGetPersona);
            oEntityCLS.listaPersona = lista;
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            string urlGetPersona = App.Current.Resources["GetPersona"].ToString();
            SwipeItem oSwipeItem = sender as SwipeItem;
            PersonaCLS oPersonaCLS = oSwipeItem.BindingContext as PersonaCLS;
            int iidpersona = oPersonaCLS.iidpersona;
           int rta =  await GenericLH.Delete(urlGetPersona+"/?iidPersonas="+iidpersona);
            if (rta == 1) listaPersonas();
            else await DisplayAlert("alerta", "No se puede eliminar existe un error", "Cancel");
        }

        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            string urlGetPersona = App.Current.Resources["GetPersona"].ToString();
            SwipeItem oSwipeItem = sender as SwipeItem;
            PersonaCLS oPersonaCLS = oSwipeItem.BindingContext as PersonaCLS;
            int iidpersona = oPersonaCLS.iidpersona;
            string getTipoUsuario = App.Current.Resources["GetTipoUsuario"].ToString();
            List<TipoUsuarioCLS> listaTipoUsuario = await GenericLH.GetAll<TipoUsuarioCLS>(getTipoUsuario);
            await Navigation.PushAsync(new FormPersona(iidpersona, "Editar Personas",listaTipoUsuario));

        }

        private async void toolBarRegis_Clicked(object sender, EventArgs e)
        {
            string getTipoUsuario = App.Current.Resources["GetTipoUsuario"].ToString();
            List<TipoUsuarioCLS> listaTipoUsuario = await GenericLH.GetAll<TipoUsuarioCLS>(getTipoUsuario);
            await Navigation.PushAsync(new FormPersona(0, "Agregar Personas",listaTipoUsuario));
        }
    }
}