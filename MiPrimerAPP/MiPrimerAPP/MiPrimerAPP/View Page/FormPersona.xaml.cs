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
    public partial class FormPersona : ContentPage
    {
        public PersonaModel oEntityCLS { get; set; }
        public string titulo { get; set; }
        public int _iidPersonas { get; set; }
        private List<TipoUsuarioCLS> listaTipoUsuario;
        public List<string> listaPicketTipoUsuario { get; set; }
       

        public FormPersona(int iidpersonas,string tituloPersonas,List<TipoUsuarioCLS> tipousuario)
        {
            InitializeComponent();
            titulo = tituloPersonas;
            oEntityCLS = new PersonaModel();
            oEntityCLS.IsVisible = false;
            oEntityCLS.IsVisibleButton = true;
            _iidPersonas = iidpersonas;
            listaTipoUsuario = tipousuario;
            listaPicketTipoUsuario = listaTipoUsuario.Select(p => p.nombre).ToList();
            oEntityCLS.oPersonaCLS = new PersonaCLS();
            BindingContext = this;

            if(iidpersonas != 0)
            {
                recuperarPersonas(iidpersonas);
            }
        }

        public async void recuperarPersonas(int iidpersonas)
        {
            string getPersona = App.Current.Resources["GetPersona"].ToString();
            oEntityCLS.oPersonaCLS = await GenericLH.Get<PersonaCLS>(getPersona+"/"+iidpersonas);
        }

        private void swichTieneUsuario_Toggled(object sender, ToggledEventArgs e)
        {
            if (oEntityCLS.oPersonaCLS.isTieneusuario == true)
            {

                oEntityCLS.IsVisibleButton = false;
                oEntityCLS.IsVisible = true;
                if (_iidPersonas == 0)
                {
                    oEntityCLS.IsVisibleContra = true;
                }
                else
                {
                    if(oEntityCLS.oPersonaCLS.iidusuario == 0)
                    {
                        oEntityCLS.IsVisibleContra = true;
                    }
                    else
                    {
                        oEntityCLS.IsVisibleContra = false;
                    }
                   
                }
            }
            else
            {
                oEntityCLS.IsVisible = false;
                oEntityCLS.IsVisibleButton = true;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            int iidtipoUsuario;
            Persona u = Persona.GetInstance();
            string obtenerURLGetPersona = App.Current.Resources["GetPersona"].ToString();
            string nombreTipo = oEntityCLS.oPersonaCLS.nombretipousuario;
            if(oEntityCLS.oPersonaCLS.isTieneusuario == true)
            {
                iidtipoUsuario = listaTipoUsuario.Where(p => p.nombre == nombreTipo).First().iidtipousuario;
                oEntityCLS.oPersonaCLS.iidtipoUsuario = iidtipoUsuario;
            }
            if (oEntityCLS.IsVisibleContra == false) oEntityCLS.oPersonaCLS.contra = "";
            else oEntityCLS.oPersonaCLS.contra = GenericLH.cifrarCadena(oEntityCLS.oPersonaCLS.contra);
            int rpta = await GenericLH.Post<PersonaCLS>(obtenerURLGetPersona, oEntityCLS.oPersonaCLS);
            if (rpta == 1)
            {
                await Navigation.PopAsync();
                u.listaPersonas();
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}