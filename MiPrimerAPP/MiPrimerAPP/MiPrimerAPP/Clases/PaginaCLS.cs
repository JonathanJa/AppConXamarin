using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerAPP.Clases
{
    public class PaginaCLS
    {
        public int iidpagina { get; set; }
        public string nombre { get; set; }
        public string icono { get; set; }
        public bool bseleccionado { get; set; }

        //agregado-------------------
        public string mensaje { get; set; }
        public string nombreicono { get; set; }
    }
}
