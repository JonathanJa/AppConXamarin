using System;
using System.Collections.Generic;
using System.Text;

namespace MiPrimerAPP.Clases
{
    public class PersonaCLS
    {
        public int iidpersona { get; set; }
        public string nombrecompleto { get; set; }
        public string fechanacimiento { get; set; }
        public string telefono { get; set; }
        public string tieneusuario { get; set; }
        //recuperar personas

        public string nombre { get; set; }
        public string appaterno { get; set; }
        public string apmaterno { get; set; }
        public string correo { get; set; }
        public DateTime fechanacimientodate { get; set; }
        public bool isTieneusuario { get; set; }

        //agregarmos nuevos elementos
        public int iidusuario { get; set; }
        public string nombreusuario { get; set; }
        public string nombretipousuario { get; set; }
        public string contra { get; set; }
        public int iidtipoUsuario { get; set; }
    }
}
