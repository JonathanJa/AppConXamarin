using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVentaCLS
    {
        public int iiddetalleventa { get; set; }
        public int iidventa { get; set; }
        public int iidproducto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
