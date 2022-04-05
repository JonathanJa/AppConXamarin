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
    public class MarcaController : ApiController
    {
        // GET api/values
        public List<MarcaCLS> Get()
        {
            MarcaDAL oMarcaDAL = new MarcaDAL();
            return oMarcaDAL.listarMarca();
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public int Post([FromBody] CategoriaCLS oCategoriaCLS)
        //{
        //    CategoriaDAL oCategoriaDAL = new CategoriaDAL();
        //    return oCategoriaDAL.guardarCategoria(oCategoriaCLS);
        //}

        ////// PUT api/values/5
        ////public void Put(int id, [FromBody] string value)
        ////{
        ////}

        //// DELETE api/values/5
        //public int Delete(int id)
        //{
        //    CategoriaDAL oCategoriaDAL = new CategoriaDAL();
        //    return oCategoriaDAL.eliminarCategoria(id);
        //}
    }
}
