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
    public partial class Menu : ContentPage
    {
        public MenuModel oEntityCLS { get; set; }
        public List<MenuCLS> listamenu { get; set; } 
        public Menu()
        {
            InitializeComponent();
            oEntityCLS = new MenuModel();
            oEntityCLS.listaMenu = new List<MenuCLS>();
            //listamenu.Add(new MenuCLS
            //{
            //    NombreItem = "Categoria",
            //    NombreIcono = "ic_categoria"
            //});
            //listamenu.Add(new MenuCLS
            //{
            //    NombreItem = "Producto",
            //    NombreIcono = "ic_producto"
            //});
            //listamenu.Add(new MenuCLS
            //{
            //    NombreItem = "Consulta",
            //    NombreIcono = "ic_search"
            //});
            //listamenu.Add(new MenuCLS
            //{
            //    NombreItem = "Usuario",
            //    NombreIcono = "ic_user"
            //});
            //listamenu.Add(new MenuCLS
            //{
            //    NombreItem = "Tipo Usuario",
            //    NombreIcono = "ic_tipoUsuario"
            //});
            //listamenu.Add(new MenuCLS
            //{
            //    NombreItem = "Salir",
            //    NombreIcono = "ic_close"
            //});
            BindingContext = this;
            listarmenu();
        }


        public async void listarmenu()
        {
            string obtenerURLPagina = App.Current.Resources["GetPagina"].ToString();
            int iidtipoUsuario = Settings.idTipoUsuario;
           List<PaginaCLS> pagina = await GenericLH.GetAll<PaginaCLS>(obtenerURLPagina + "/" + iidtipoUsuario);
            pagina.Add(new PaginaCLS(){nombre = "Salir",icono = "ic_close"});
            listamenu = pagina.Select(p => new MenuCLS { NombreIcono = p.icono, NombreItem = p.nombre }).ToList();
            oEntityCLS.listaMenu = listamenu;

        }

        //private void lstmenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    MenuCLS omenuCLS = (MenuCLS)e.SelectedItem;
        //    switch (omenuCLS.NombreItem)
        //    {
        //        case "Categoria": App.Navigation.PushAsync(new Categoria());break;
        //        case "Producto": App.Navigation.PushAsync(new Producto()); break;
        //        case "Consulta": App.Navigation.PushAsync(new TabbedPagesConsultas()); break;
        //        case "Usuario": App.Navigation.PushAsync(new Persona()); break;
        //        case "Tipo Usuario": App.Navigation.PushAsync(new TipoUsuario()); break;
        //        case "Pedido": App.Navigation.PushAsync(new Pedido()); break;
        //        case "Salir": Application.Current.MainPage = new MainPage(); Settings.RecordarContra = false; break;
        //    }

        //    //cerrar el menu 

        //    App.MenuApp.IsPresented = false;
        //}

        private void lstmenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MenuCLS omenuCLS = (MenuCLS)e.Item;
            switch (omenuCLS.NombreItem)
            {
                case "Categoria": App.Navigation.PushAsync(new Categoria()); break;
                case "Producto": App.Navigation.PushAsync(new Producto()); break;
                case "Consulta": App.Navigation.PushAsync(new TabbedPagesConsultas()); break;
                case "Usuario": App.Navigation.PushAsync(new Persona()); break;
                case "Tipo Usuario": App.Navigation.PushAsync(new TipoUsuario()); break;
                case "Pedido": App.Navigation.PushAsync(new Pedido()); break;
                case "Salir": Application.Current.MainPage = new MainPage(); Settings.RecordarContra = false; break;
            }

            //cerrar el menu 

            App.MenuApp.IsPresented = false;
        }
    }
}