using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MiPrimerAPP.Clases
{
    public class CategoriaCLS
    {
        public int iidcategoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }
        [JsonIgnore]
        public ImageSource ListaFotoString { get; set; }
    }
}
