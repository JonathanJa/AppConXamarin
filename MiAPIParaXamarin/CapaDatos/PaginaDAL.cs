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
    public class PaginaDAL:CadenaDAL
    {
        public List<PaginaCLS> recuperarPagina(int iidpagina)
        {
            List<PaginaCLS> lista = null;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspListarPaginasTipoUsuario", cn))
                    {
                        //indico que era un procedure
                        cmd.Parameters.AddWithValue("@iidtipousuario", iidpagina);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<PaginaCLS>();
                            PaginaCLS oPaginaCLS;
                            while (drd.Read())
                            {
                                oPaginaCLS = new PaginaCLS();
                                oPaginaCLS.iidpagina = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oPaginaCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oPaginaCLS.icono = drd.IsDBNull(2) ? "" : drd.GetString(2);

                                lista.Add(oPaginaCLS);
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

        public List<PaginaCLS> listarPagina()
        {
            List<PaginaCLS> lista = null;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspListarPaginas", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<PaginaCLS>();
                            PaginaCLS oPaginaCLS;
                            while (drd.Read())
                            {
                                oPaginaCLS = new PaginaCLS();
                                oPaginaCLS.iidpagina = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oPaginaCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oPaginaCLS.nombreicono = drd.IsDBNull(2) ? "" : drd.GetString(2);

                                lista.Add(oPaginaCLS);
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


        public int guardarPagina(PaginaCLS oPaginaCLS)
        {
            int rta = 0;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarPagina", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidpagina", oPaginaCLS.iidpagina);
                        cmd.Parameters.AddWithValue("@mensaje", oPaginaCLS.nombre);
                        cmd.Parameters.AddWithValue("@nombreicono", oPaginaCLS.nombreicono);
                        
                        //INSERT,UPDATE O DELETE
                        //El numero de registro afectados (1) ->(0)
                        rta = cmd.ExecuteNonQuery();

                        cn.Close();
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    rta = 0;
                }
            }

            return rta;

        }

        public int eliminarPagina(int idpagina)
        {
            int rta = 0;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarPagina", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idPagina", idpagina);
                        //INSERT,UPDATE O DELETE
                        //El numero de registro afectados (1) ->(0)
                        rta = cmd.ExecuteNonQuery();

                        cn.Close();
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    rta = 0;
                }
            }

            return rta;

        }
    }
}
