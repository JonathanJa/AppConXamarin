using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MiPrimerAPP.Clases
{
    public class ProductoCLS
    {
        public int iidproducto { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string nombrecategoria { get; set; }
        public string nombremarca { get; set; }
        public int iidcategoria { get; set; }
        public int iidmarca { get; set; }
        public byte[] foto { get; set; }
        [JsonIgnore]
        public ImageSource ListaFotoString { get; set; }
       
    }
}
