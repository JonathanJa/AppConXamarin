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
    public class UsuarioController : ApiController
    {
        // GET api/values
        public UsuarioCLS Get(string usuario,string contra)
        {
            UsuarioDAL oUsuarioDAL = new UsuarioDAL();
            return oUsuarioDAL.Login(usuario,contra);
        }

        //// GET api/values/5
        //public PersonaCLS Get(int id)
        //{
        //    PersonaDAL oPersonaDAL = new PersonaDAL();
        //    return oPersonaDAL.recuperarPersona(id);
        //}

        ////POST api/values
        //public int Post([FromBody] PersonaCLS oPersonaCLS)
        //{
        //    PersonaDAL oPersonaDAL = new PersonaDAL();
        //    return oPersonaDAL.guardarPersona(oPersonaCLS);
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        public int Delete(int iidPersonas)
        {
            PersonaDAL oPersonaDAL = new PersonaDAL();
            return oPersonaDAL.eliminarPersona(iidPersonas);
        }
    }
}
