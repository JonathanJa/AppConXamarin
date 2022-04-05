using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MiPrimerAPP.Model
{
    public class PaginaModel : BaseBinding
    {
        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }

        private List<PaginaCLS> _listaPagina;
        public List<PaginaCLS> listaPagina
        {
            get { return _listaPagina; }
            set { SetValue(ref _listaPagina, value); }
        }
    }
}
