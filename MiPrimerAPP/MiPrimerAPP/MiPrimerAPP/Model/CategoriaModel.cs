using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MiPrimerAPP.Model
{
    public class CategoriaModel : BaseBinding
    {
        private bool _IsLoading;
        public bool IsLoading
        {
            get {return _IsLoading;}
            set{SetValue(ref _IsLoading, value);}
        }

        private ImageSource _Image;
        public ImageSource Image
        {
            get { return _Image; }
            set { SetValue(ref _Image, value); }
        }



        //categoria
        private List<CategoriaCLS> _listaCategoria;
        public List<CategoriaCLS> listaCategoria
        {
            get{ return _listaCategoria;}
            set{SetValue(ref _listaCategoria, value);}
        }

        
    }
}
