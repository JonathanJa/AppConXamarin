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
    public class CategoriaMarcaController : ApiController
    {
        // GET api/values
        public CategoriaMarcaCLS Get()
        {
            CategoriaDAL oCategoriaDAL = new CategoriaDAL();
            MarcaDAL oMarcaDAL = new MarcaDAL();
            CategoriaMarcaCLS obj = new CategoriaMarcaCLS();
            obj.listarCategoria = oCategoriaDAL.listarCategoria();
            obj.listarMarca = oMarcaDAL.listarMarca();
            return obj;
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
