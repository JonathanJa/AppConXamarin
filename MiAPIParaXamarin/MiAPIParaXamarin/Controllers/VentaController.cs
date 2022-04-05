using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MiAPIParaXamarin.Controllers
{
    public class VentaController : ApiController
    {
        // GET api/values
        //public List<CategoriaCLS> Get()
        //{
        //    CategoriaDAL oCategoriaDAL = new CategoriaDAL();
        //    return oCategoriaDAL.listarCategoria();
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public int Post([FromBody] CompraCLS oCompraCLS)
        {
            VentaDAL oVentaDAL = new VentaDAL();
            return oVentaDAL.guardarVenta(oCompraCLS);
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        //public int Delete(int id)
        //{
        //    CategoriaDAL oCategoriaDAL = new CategoriaDAL();
        //    return oCategoriaDAL.eliminarCategoria(id);
        //}
    }
}
