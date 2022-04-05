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
    public class PaginaController : ApiController
    {
        // GET api/values
        public List<PaginaCLS> Get()
        {
            PaginaDAL oPaginaDAL = new PaginaDAL();
            return oPaginaDAL.listarPagina();
        }

        // GET api/values/5
        public List<PaginaCLS> Get(int id)
        {
            PaginaDAL oPaginaDAL = new PaginaDAL();
            return oPaginaDAL.recuperarPagina(id);
        }

        // POST api/values
        public int Post([FromBody] PaginaCLS oPaginaCLS)
        {
            PaginaDAL oPaginaDAL = new PaginaDAL();
            return oPaginaDAL.guardarPagina(oPaginaCLS);
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        public int Delete(int idpagina)
        {
            PaginaDAL oPaginaDAL = new PaginaDAL();
            return oPaginaDAL.eliminarPagina(idpagina);
        }
    }
}
