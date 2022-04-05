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
    public partial class Detail : ContentPage
    {
        
        public Detail()
        {
            InitializeComponent();
            ListarDatos();
        }

        public async void ListarDatos()
        {
            string urlGetCategoria = App.Current.Resources["GetCategoria"].ToString();
            List<CategoriaCLS> l = await GenericLH.GetAll<CategoriaCLS>(urlGetCategoria);
            foreach (CategoriaCLS obj in l) obj.ListaFotoString = ImageSource.FromFile("ic_nofoto.png");
            foreach (CategoriaCLS obj in l.Where(p => p.foto != "")) obj.ListaFotoString = GenericLH.convertirBase64aImageSource(obj.foto);
            StackLayout oStacklayout;
            Frame oFrame;
            Image oImage;
            Label oLabel;
            
            int sum = 10;
            int contador = 0;
            TapGestureRecognizer recognize;
            
            foreach (CategoriaCLS oCategoriaCLS in l)
            {
               
                
                contador++;
                if(contador == 3)
                {
                    sum += 10;
                    contador=1;
                }
                recognize = new TapGestureRecognizer();
                recognize.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new ProductoList(oCategoriaCLS.nombre,oCategoriaCLS.iidcategoria));
                };

                oImage = new Image();
                oImage.Source = oCategoriaCLS.ListaFotoString;
                oImage.HorizontalOptions = LayoutOptions.Center;
                oImage.HeightRequest = 120;
                oImage.WidthRequest = 120;
                oLabel = new Label();
                oLabel.Text = oCategoriaCLS.nombre;
                oLabel.HorizontalTextAlignment = TextAlignment.Center;
                oStacklayout = new StackLayout();
                oStacklayout.Children.Add(oImage);
                oStacklayout.Children.Add(oLabel);
                
                oFrame = new Frame();
                oFrame.Margin = new Thickness(0, sum, 0, 0);
                oFrame.Content = oStacklayout;
                oFrame.GestureRecognizers.Add(recognize);
                FlexContainer.Children.Add(oFrame);

            }
        }

        private async void toolAgregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pagina());
        }
    }
}