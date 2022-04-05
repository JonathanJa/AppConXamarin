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
    public partial class FormCategoria : ContentPage
    {
        public CategoriaModel oCategoriaModel { get; set; } = new CategoriaModel();
        public CategoriaCLS oCategoriaCLS { get; set; } = new CategoriaCLS();
        public string titulo { get; set; }
        private MediaFile oMediafile;
        public FormCategoria(CategoriaCLS obj,string nombretitulo)
        {
            InitializeComponent();
            oCategoriaModel.Image = null;
            oCategoriaCLS = obj;
            oCategoriaModel.Image = obj.foto == null? null : GenericLH.convertirBase64aImageSource(obj.foto);
           titulo = nombretitulo;
            BindingContext = this;
        }

        private async void btnAceptar_Clicked(object sender, EventArgs e)
        {
            Categoria obj = Categoria.GetInstance();
            List<CategoriaCLS> l = obj.oEntityCLS.listaCategoria.ToList();
            string urlGetCategoria = App.Current.Resources["GetCategoria"].ToString();
            ErrorCLS oError =  GenericLH.ObligatorioTexto(limpiarCategoria);
            if(oError.exito == false)
            {
                await DisplayAlert("Alerta", oError.mensaje, "cancelar");
                return;
            }
            if(oMediafile != null)
            {
                
                    oCategoriaCLS.foto = GenericLH.convertirMediaFileABase64(oMediafile);
                
            }
            var rpta = await GenericLH.Post(urlGetCategoria, oCategoriaCLS);
            if (rpta != 0) 
            {
                await Navigation.PopAsync();
                obj.listarCategoria();

            }


            //HttpClient cliente = new HttpClient();
            //var cara = JsonConvert.SerializeObject(oCategoriaCLS);
            //var cuerpo = new StringContent(cara, Encoding.UTF8, "application/json");
            //var responsive = await cliente.PostAsync("http://juan21-001-site1.ctempurl.com/api/categoria",cuerpo);
            //if (!responsive.IsSuccessStatusCode) await DisplayAlert("error", "no se ha podido guardar", "cancel");
            //else
            //{
            //   int res = int.Parse( await responsive.Content.ReadAsStringAsync());
            //    if (res == 0) await DisplayAlert("error", "no se ha podido guardar en la base de datos", "ok");
            //    else 
            //    {
            //        await Navigation.PopAsync();
            //        obj.listarCategoria();

            //    }

            //}
            //if(titulo == "Agregar Categoria")
            //{
            //    //agregar
            //    l.Add(oCategoriaCLS);
            //}
            //else
            //{
            //    //editar
            //    int indixe = l.FindIndex(p => p.Iidcategoria == oCategoriaCLS.Iidcategoria);
            //    l[indixe] = oCategoriaCLS;
            //}


            //obj.oEntityCLS.listaCategoria = l;
            //Navigation.PopAsync();
        }

        private async void btnAtras_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnLimpiar_Clicked(object sender, EventArgs e)
        {
             GenericLH.limpiarTexto(limpiarCategoria);
        }

        private async void imagePreviewCategoria_Tapped(object sender, EventArgs e)
        {
            //iniciamos la camara
            await CrossMedia.Current.Initialize();
            string opcion = await DisplayActionSheet("Elija una opcion", "Cancelar", null, "Galeria", "Camara");
            switch (opcion)
            {
                case "Galeria":

                    oMediafile=await CrossMedia.Current.PickPhotoAsync();

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
                    ;break;

            }
            if(oMediafile != null) { oCategoriaModel.Image = GenericLH.convertirMediaFileAImageSource(oMediafile); }
            
        }
    }
}