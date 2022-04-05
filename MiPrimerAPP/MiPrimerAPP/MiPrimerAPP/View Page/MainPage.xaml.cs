using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using MiPrimerAPP.View_Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiPrimerAPP
{
    public partial class MainPage : ContentPage
    {
        public UsuarioCLS oUsuarioCLS { get; set; } = new UsuarioCLS();
        public bool recordarContra { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void verMas_Clicked(object sender, EventArgs e)
        {
            //string mensaje = lblver.Text;
            //1. ocultar el boton
            verMas.IsVisible = false;
            //2.mostrar el texto
            lblver.LineBreakMode = LineBreakMode.WordWrap;
            //Application.Current.MainPage.DisplayAlert("Alerta", mensaje, "Cancel");
        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroUsuario());
        }

        private async void toolBarRegis_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroUsuario());
        }

        private async void btnIngresa_Clicked(object sender, EventArgs e)
        {
            string obtenerurlUsuario = App.Current.Resources["GetUsuario"].ToString();
            string usuario = oUsuarioCLS.NombreUsuario;
            string contra = oUsuarioCLS.Contra;
            string cifradoContra = GenericLH.cifrarCadena(contra);
            UsuarioCLS osu = await GenericLH.Get<UsuarioCLS>(obtenerurlUsuario + "/?usuario=" + usuario + "&contra=" + cifradoContra);
            if(osu.iidusuario != 0)
            
            {
                Settings.idUsuario = osu.iidusuario;
                Settings.idTipoUsuario = osu.iidtipousuario;
                Settings.RecordarContra = recordarContra;
                Application.Current.MainPage = new PaginaPrincipal();
            }
            else
            {
                Settings.idUsuario = 0;
                Settings.idTipoUsuario = 0;
                Settings.RecordarContra = false;
                await DisplayAlert("error", "El usuario y/o contraseña son incorrecto", "ok");
            }
        }

        private void BtnAsignar_Clicked(object sender, EventArgs e)
        {
            oUsuarioCLS.NombreUsuario = "lhurol";
            oUsuarioCLS.Contra = "12345678";
        }
    }
}
