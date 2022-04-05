using MiPrimerAPP.Clases;
using MiPrimerAPP.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;


namespace MiPrimerAPP.Model
{
    public class PedidoModel : BaseBinding
    {
        private List<DetalleVentaCLS> _listaProductos;
        public List<DetalleVentaCLS> listaProductos
        {
            get { return _listaProductos; }
            set { SetValue(ref _listaProductos, value); }
        }

        private VentasCLS _oVentasCLS;
        public VentasCLS oVentasCLS
        {
            get { return _oVentasCLS; }
            set { SetValue(ref _oVentasCLS, value); }
        }

    }
}
