using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//bd
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace CapaDatos
{
    public class TipoUsuarioDAL:CadenaDAL
    {
        public TipoUsuarioCLS recuperarTipoUsuario(int iidtipoUsuario)
        {
            TipoUsuarioCLS oTipoUsuarioCLS = new TipoUsuarioCLS();
            PaginaDAL oPaginaDAL = new PaginaDAL();
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarTipoUsuario", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidtipousuario", iidtipoUsuario);
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {


                            while (drd.Read())
                            {

                                oTipoUsuarioCLS.iidtipousuario = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oTipoUsuarioCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oTipoUsuarioCLS.descripcion = drd.IsDBNull(2) ? "" : drd.GetString(2);


                            }
                            
                        }
                        
                            List<PaginaCLS> listaPagina = oPaginaDAL.listarPagina();
                        if (drd.NextResult())
                        {
                            while (drd.Read())
                            {

                                int iidpagina = drd.IsDBNull(1) ? 0 : drd.GetInt32(1);
                                listaPagina.Where(p => p.iidpagina == iidpagina).First().bseleccionado = true;


                            }
                        }
                        oTipoUsuarioCLS.listaPagina = listaPagina;
                        cn.Close();
                    }

                }
                catch (Exception)
                {

                    cn.Close();

                }
            }
            return oTipoUsuarioCLS;
        }
        public int guardarTipoUsuario(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            int rpta = 0;
            int iidAutogenerado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlTransaction transaccion = cn.BeginTransaction())
                    {
                        SqlParameter parametro = null;
                        using (SqlCommand cmd = new SqlCommand("uspGuardarTipoUsuario", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iidtipousuario", oTipoUsuarioCLS.iidtipousuario);
                            cmd.Parameters.AddWithValue("@nombre", oTipoUsuarioCLS.nombre);
                            cmd.Parameters.AddWithValue("@descripcion", oTipoUsuarioCLS.descripcion);
                            if (oTipoUsuarioCLS.iidtipousuario == 0)
                            {
                                parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                                parametro.Direction = ParameterDirection.ReturnValue;
                            }
                            rpta = cmd.ExecuteNonQuery();
                            if (rpta == 1 && oTipoUsuarioCLS.iidtipousuario == 0)
                            {
                                iidAutogenerado = (int)parametro.Value;
                                oTipoUsuarioCLS.iidtipousuario = iidAutogenerado;
                            }

                        }
                        using (SqlCommand cmd = new SqlCommand("uspDesabilitarPagina", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iidtipousuario",oTipoUsuarioCLS.iidtipousuario);
                            cmd.ExecuteNonQuery();
                        }
                        List<int> listaInt = oTipoUsuarioCLS.iidtipousuarioList;
                        foreach (int id in listaInt)
                        {
                            using (SqlCommand cmd = new SqlCommand("uspGuardarTipoUsuarioPagina", cn, transaccion))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@iidtipousuario", oTipoUsuarioCLS.iidtipousuario);
                                cmd.Parameters.AddWithValue("@iidpagina", id);
                                rpta = cmd.ExecuteNonQuery();
                                if (rpta == 0) return rpta;
                            }

                        }
                        transaccion.Commit();
                        cn.Close();

                    }

                }
                catch (Exception ex)
                {
                    rpta = 0;
                    cn.Close();
                }
            }
            return rpta;
        }

        public List<TipoUsuarioCLS> listarTipoUsuario()
        {
            List<TipoUsuarioCLS> lista = null;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("usplistarTipoUsuario", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<TipoUsuarioCLS>();
                            TipoUsuarioCLS oTipoUsuarioCLS;
                            while (drd.Read())
                            {
                                oTipoUsuarioCLS = new TipoUsuarioCLS();
                                oTipoUsuarioCLS.iidtipousuario = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oTipoUsuarioCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                lista.Add(oTipoUsuarioCLS);
                            }
                            cn.Close();
                        }
                    }

                }
                catch (Exception)
                {
                    lista = null;
                    cn.Close();

                }
            }
            return lista;
        }
        //public int guardarTipoUsuario(TipoUsuarioCLS oTipoUsuarioCLS)
        //{
        //    int rpta = 0;
        //    int iidAutogenerado = 0;
        //    using (SqlConnection cn = new SqlConnection(cadena))
        //    {
        //        try
        //        {
        //            cn.Open();
        //            using (SqlTransaction transaccion = cn.BeginTransaction())
        //            {
        //                SqlParameter parametro = null;
        //                using (SqlCommand cmd = new SqlCommand("uspGuardarTipoUsuario", cn, transaccion))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@iidtipousuario", oTipoUsuarioCLS.iidtipousuario);
        //                    cmd.Parameters.AddWithValue("@nombre", oTipoUsuarioCLS.nombre);
        //                    cmd.Parameters.AddWithValue("@descripcion", oTipoUsuarioCLS.descripcion);
                           

        //                    if (oTipoUsuarioCLS.iidtipousuario == 0)
        //                    {
        //                        //Nuevo
        //                        parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
        //                        parametro.Direction = ParameterDirection.ReturnValue;
        //                    }
        //                    rpta = cmd.ExecuteNonQuery();
        //                    if (rpta == 1 && oTipoUsuarioCLS.iidtipousuario == 0)
        //                    {
        //                        iidAutogenerado = (int)parametro.Value;
        //                        oTipoUsuarioCLS.iidtipousuario= iidAutogenerado;
        //                    }
        //                }
        //                List<int> listaint = oTipoUsuarioCLS.iidtipousuarioList;
        //                //crear el usuario es opcional
        //                foreach (int id in listaint)
        //                {

        //                    using (SqlCommand cmd = new SqlCommand("uspGuardarTipoUsuarioPagina", cn, transaccion))
        //                    {
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.AddWithValue("@iidusuario", oTipoUsuarioCLS.iidtipousuario);
        //                        cmd.Parameters.AddWithValue("@iidpagina",id);
        //                        rpta = cmd.ExecuteNonQuery();
        //                        if (rpta == 0) return rpta;
        //                    }


        //                }


        //                //Esto ejecuta
        //                transaccion.Commit();
        //                cn.Close();

        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            rpta = 0;
        //            cn.Close();
        //        }


        //    }
        //    return rpta;
        //}
    }
}
