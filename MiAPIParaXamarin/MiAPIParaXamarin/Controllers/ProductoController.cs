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
    public class ProductoController : ApiController
    {
        // GET api/values
        public List<ProductoCLS> Get()
        {
            ProductoDAL oProductoDAL = new ProductoDAL();
            return oProductoDAL.listarProducto();
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public int Post([FromBody] ProductoCLS oProductoCLS)
        {
            ProductoDAL oProductoDAL = new ProductoDAL();
            return oProductoDAL.guardarProducto(oProductoCLS);
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        public int Delete(int id)
        {
            ProductoDAL oProductoDAL = new ProductoDAL();
            return oProductoDAL.eliminarProducto(id);
        }
    }
}
