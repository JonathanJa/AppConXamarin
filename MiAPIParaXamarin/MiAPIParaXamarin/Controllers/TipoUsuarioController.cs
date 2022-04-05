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
    public class TipoUsuarioController : ApiController
    {
        // GET api/values
        public List<TipoUsuarioCLS> Get()
        {
            TipoUsuarioDAL oTipoUsuarioDAL = new TipoUsuarioDAL();
            return oTipoUsuarioDAL.listarTipoUsuario();
        }

        //// GET api/values/5
        public TipoUsuarioCLS Get(int id)
        {
            TipoUsuarioDAL oTipoUsuarioDAL = new TipoUsuarioDAL();
            return oTipoUsuarioDAL.recuperarTipoUsuario(id);
        }

        //// POST api/values
        public int Post([FromBody] TipoUsuarioCLS oTipoUsuarioCLS)
        {
            TipoUsuarioDAL oTipoUsuarioDAL = new TipoUsuarioDAL();
            return oTipoUsuarioDAL.guardarTipoUsuario(oTipoUsuarioCLS);
        }

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
