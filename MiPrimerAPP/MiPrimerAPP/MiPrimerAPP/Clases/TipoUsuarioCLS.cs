using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerAPP.Clases
{
    public class TipoUsuarioCLS
    {
        public int iidtipousuario { get; set; }
        public string nombre { get; set; }

        public string descripcion { get; set; }
        public List<int> iidtipousuarioList { get; set; }

        public List<PaginaCLS> listaPagina { get; set; }
    }
}
