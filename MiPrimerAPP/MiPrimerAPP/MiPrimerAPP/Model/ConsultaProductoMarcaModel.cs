using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerAPP.Model
{
    public class ConsultaProductoMarcaModel:BaseBinding
    {
        private List<String> _listaMarca;
        public List<String> listaMarca
        {
            get { return _listaMarca; }
            set { SetValue(ref _listaMarca, value); }
        }


        private List<ProductoCLS> _listaProductos;
        public List<ProductoCLS> listaProductos
        {
            get { return _listaProductos; }
            set { SetValue(ref _listaProductos, value); }
        }


        private String _NombreMarca;
        public String NombreMarca
        {
            get { return _NombreMarca; }
            set { SetValue(ref _NombreMarca, value); }
        }
    }
}
