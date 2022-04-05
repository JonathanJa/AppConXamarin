using MiPrimerAPP.Generic;
using MiPrimerAPP.View_Page;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiPrimerAPP
{
    public partial class App : Application
    {
        public static NavigationPage Navigation { get; internal set; }
        public static PaginaPrincipal MenuApp { get; internal set; }

        public App()
        {
            InitializeComponent();
            //ejecucion de paginas de contenidos(importante agregar new NavigationPage )
            if(Settings.RecordarContra == true)
            {
                Application.Current.MainPage = new PaginaPrincipal();
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());

            }
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
