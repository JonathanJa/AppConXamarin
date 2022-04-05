using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerAPP.Model
{
    public class ConsultaProductoCategoriaModel:BaseBinding
    {

        private List<String> _listaCategoria;
        public List<String> listaCategoria
        {
            get { return _listaCategoria; }
            set { SetValue(ref _listaCategoria, value); }
        }


        private List<ProductoCLS> _listaProductos;
        public List<ProductoCLS> listaProductos
        {
            get { return _listaProductos; }
            set { SetValue(ref _listaProductos, value); }
        }


        private String _NombreCategoria;
        public String NombreCategoria
        {
            get { return _NombreCategoria; }
            set { SetValue(ref _NombreCategoria, value); }
        }

        private CategoriaModel _OCategoriaModel;
        public CategoriaModel OCategoriaModel
        {
            get { return _OCategoriaModel; }
            set { SetValue(ref _OCategoriaModel, value); }
        }

    }
}
