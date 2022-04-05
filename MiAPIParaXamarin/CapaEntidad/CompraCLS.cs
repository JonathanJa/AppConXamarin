using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CompraCLS
    {
        public VentasCLS oVentasCLS { get; set; }
        public List<DetalleVentaCLS> listadetalles { get; set; }
    }
}
