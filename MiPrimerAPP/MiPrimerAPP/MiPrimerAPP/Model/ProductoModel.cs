using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MiPrimerAPP.Model
{
    public class ProductoModel : BaseBinding
    {
        private bool _IsLoadingForm;
        public bool IsLoadingForm
        {
            get { return _IsLoadingForm; }
            set { SetValue(ref _IsLoadingForm, value); }
        }

        private ProductoCLS _oProductoCLS;
        public ProductoCLS oProductoCLS {
            get {return _oProductoCLS;} 
            set{SetValue(ref _oProductoCLS, value);}
          }


        private List<String> _listaCategoria;
        public List<String> listaCategoria {
            get{return _listaCategoria;}
            set{SetValue(ref _listaCategoria, value);}
        }

        private List<String> _listaMarca;
        public List<String> listaMarca
        {
            get{return _listaMarca;}
            set{SetValue(ref _listaMarca, value);}
        }
        private bool _IsLoading;
        public bool IsLoading
        {
            get{return _IsLoading;}
            set{SetValue(ref _IsLoading, value);}
        }

        private List<ProductoCLS> _listaProductos;
        public List<ProductoCLS> listaProductos
        {
            get{return _listaProductos;}
            set{SetValue(ref _listaProductos, value);}
        }
        private ImageSource _Image;
        public ImageSource Image
        {
            get { return _Image; }
            set { SetValue(ref _Image, value); }
        }


    }
}
