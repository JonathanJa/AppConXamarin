using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MiPrimerAPP.Model;

namespace MiPrimerAPP.View_Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoList : ContentPage
    {
        public ProductoModel oEntityCLS { get; set; }
        public string titulo { get; set; }
        private int idProd;
        public ProductoList(string nombretitulo,int idpro)
        {
            
            InitializeComponent();
			oEntityCLS = new ProductoModel();
			titulo = nombretitulo;
			idProd = idpro;
			BindingContext = this;
            listadoProducto();
			
		}

        public async void listadoProducto()
        {
			
			List<DetalleVentaCLS> listapro = null;
			List<int> listaIdProductos;
			if (Settings.ProductListAdd != "")
			{
				listapro = JsonConvert.DeserializeObject<List<DetalleVentaCLS>>(Settings.ProductListAdd);
			}
			else
			{
				listapro = new List<DetalleVentaCLS>();
			}
				listaIdProductos = listapro.Select(p => p.iidproducto).ToList();
			
			string urlGetProducto = App.Current.Resources["GetProducto"].ToString();
			List<ProductoCLS> lista = await GenericLH.GetAll<ProductoCLS>
				(urlGetProducto);
			foreach (ProductoCLS objProd in lista) objProd.ListaFotoString= ImageSource.FromFile("ic_nofoto.png");
			foreach (ProductoCLS objProd in lista.Where(p => p.foto != null))
				objProd.ListaFotoString = GenericLH.convertirArrayDeBytesAImagenSource(objProd.foto);
			List<ProductoCLS> listaFiltrada = lista.Where(p => p.nombrecategoria == titulo).ToList();
			

			Stepper oStepper;
			Label oCantidad;
			Frame oFrame;
			StackLayout oStackLayout;
			Image oImage;
			Label oLabel;
			int sum = 10;
			int iteracion = 0;
			Label oLabelPrecio;
			Label oLabelStock;
			BoxView oBoxView;
			Button btnAgregar;
			Button btnEliminar;
			ScrollView oScrollView;
			foreach (ProductoCLS oProductoCLS in listaFiltrada)
			{
				iteracion++;
				if (iteracion == 3)
				{
					sum += 10;
					iteracion = 1;
				}
				
				oFrame = new Frame();
				oScrollView = new ScrollView();
				oFrame.ClassId = "frm" + oProductoCLS.iidproducto;
				oFrame.WidthRequest = 125;
				//oFrame.HeightRequest = 150;
				if (listaIdProductos.Contains(oProductoCLS.iidproducto)) oFrame.BackgroundColor = Color.Aqua;
				else oFrame.BackgroundColor = Color.White;
				oFrame.Margin = new Thickness(0, sum, 0, 0);
				oStackLayout = new StackLayout();
				oImage = new Image();
				oImage.WidthRequest = 120;
				oImage.HeightRequest = 120;
				oImage.Source = oProductoCLS.ListaFotoString;
				oLabel = new Label();
				oLabel.Text = oProductoCLS.nombre;
				oLabel.HorizontalTextAlignment = TextAlignment.Center;
				oCantidad = new Label();
				oCantidad.Text = "Cantidad: 1";
				oCantidad.FontAttributes = FontAttributes.Bold;
				oCantidad.HorizontalTextAlignment = TextAlignment.Center;
				oCantidad.ClassId = "valor";
				oStepper = new Stepper();
				oStepper.Minimum = 1;
				oStepper.Maximum = oProductoCLS.stock;
				oStepper.Increment = 1;
				oStepper.Value = 1;
				oStepper.ValueChanged += (s, e) =>
				{

					Label oLabelV = (Label)GenericLH.buscarHermano((Stepper)s, "valor");
					oLabelV.Text = "Cantidad : " + e.NewValue;
					//Stepper objStepper = (Stepper)s;
					//StackLayout cont = (StackLayout)objStepper.Parent;
					//List<View> list = cont.Children.ToList();
					//foreach(View o in list)
					//               {
					//	if(o.ClassId == "valor")
					//                   {
					//		Label oLabelV = (Label)o;
					//		oLabelV.Text = "Cantidad : " + e.NewValue;


					//                   }
					//               }
				};
				oBoxView = new BoxView();
				oBoxView.BackgroundColor = Color.Tomato;
				oBoxView.HeightRequest = 3;
				oLabelPrecio = new Label();
				oLabelPrecio.Text = "Precio: $/" + oProductoCLS.precio;
				oLabelStock = new Label();
				oLabelStock.Text = "Stock: " + oProductoCLS.stock;

				btnAgregar = new Button();
				btnAgregar.ClassId ="btnagregar" + oProductoCLS.iidproducto;
				btnAgregar.Text = "Agregar";
				btnAgregar.BackgroundColor = Color.Green;
				btnAgregar.TextColor = Color.White;
				btnAgregar.Clicked += (s, e) => {

					Label oLabelV = (Label)GenericLH.buscarHermano((Button)s, "valor");
					string texto = oLabelV.Text.Replace("Cantidad : ","");	

					Frame frameSeleccionado = ((Frame)(((Button)s).Parent.Parent));
					if (frameSeleccionado.BackgroundColor == Color.Aqua)
					{
						DisplayAlert("Alerta", "No se pude Agregar por que el producto ya esta seleccionado", "cancelar");
						return;
					}
					//List<DetalleVentaCLS> listaProd = null;
					string idbotonAgregar = ((Button)s).ClassId;
					int iidproducto =int.Parse( idbotonAgregar.Replace("btnagregar", ""));
					DetalleVentaCLS oProd = listaFiltrada.Where(p => p.iidproducto == iidproducto).
					Select(p=> new DetalleVentaCLS { iidproducto = p.iidproducto, nombre=p.nombre, precio=p.precio,cantidad =1}).First();
					if (Settings.ProductListAdd == "") listapro = new List<DetalleVentaCLS>();
					else listapro = JsonConvert.DeserializeObject<List<DetalleVentaCLS>>(Settings.ProductListAdd);

					((Frame)(((Button)s).Parent.Parent)).BackgroundColor = Color.Aqua;
					listapro.Add(new DetalleVentaCLS
					{
						iidproducto = iidproducto,
						nombre = oProd.nombre,
						precio = oProd.precio,
						cantidad = int.Parse(texto)
					}) ;

					Settings.ProductListAdd = JsonConvert.SerializeObject(listapro);
				
				};

				btnEliminar = new Button();
				btnEliminar.ClassId = "btnEliminar" + oProductoCLS.iidproducto;
				btnEliminar.Text = "Eliminar";
				btnEliminar.BackgroundColor = Color.Red;
				btnEliminar.TextColor = Color.White;
				btnEliminar.Clicked += (s, e) => {
					//Frame frameSeleccionado = new Frame();
					Frame frameSeleccionado = ((Frame)(((Button)s).Parent.Parent));
                    if (frameSeleccionado.BackgroundColor == Color.White)
                    {
						DisplayAlert("Alerta", "No se pude eliminar por que el producto no esta seleccionado", "cancelar");
						return;
                    }
					string idbotonEliminar = ((Button)s).ClassId;
					int iidproducto = int.Parse(idbotonEliminar.Replace("btnEliminar", ""));
					if (Settings.ProductListAdd != "")
					{
						listapro = JsonConvert.DeserializeObject<List<DetalleVentaCLS>>(Settings.ProductListAdd);
						List<DetalleVentaCLS> listanew = listapro.Where(p=>p.iidproducto != iidproducto).ToList();
						Settings.ProductListAdd = JsonConvert.SerializeObject(listanew);
					}
					frameSeleccionado.BackgroundColor = Color.White;

				};




				oStackLayout.Children.Add(oImage);
				oStackLayout.Children.Add(oLabel);
				oStackLayout.Children.Add(oCantidad);
				oStackLayout.Children.Add(oStepper);
				oStackLayout.Children.Add(oBoxView);
				oStackLayout.Children.Add(oLabelPrecio);
				oStackLayout.Children.Add(oLabelStock);
				oStackLayout.Children.Add(btnAgregar);
				oStackLayout.Children.Add(btnEliminar);
				oFrame.Content = oStackLayout;
				oScrollView.Content = oFrame;
				flexContainer.Children.Add(oScrollView);
			}
		}
    }
}