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
    public class CategoriaDAL:CadenaDAL
    {
        //-------------------------------Eliminar las Categorias-----------------------------------------
        public int eliminarCategoria(int idcategoria)
        {
            int rta = 0;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena)) 
            {
                try 
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarCategoria", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
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
        //-----------------------------------------------------------------------------------------------
        //-------------------------------Listar las Categorias-------------------------------------------
        public List<CategoriaCLS> listarCategoria()
        {
            List<CategoriaCLS> lista = null;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn=new SqlConnection(cadena)) 
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspListarCategoria", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<CategoriaCLS>();
                            CategoriaCLS oCategoriaCLS;
                            while (drd.Read())
                            {
                                oCategoriaCLS = new CategoriaCLS();
                                oCategoriaCLS.iidcategoria = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oCategoriaCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oCategoriaCLS.descripcion = drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oCategoriaCLS.foto = drd.IsDBNull(3) ? "" : drd.GetString(3);
                                lista.Add(oCategoriaCLS);
                            }
                            cn.Close();
                        }
                    }

                }
                catch (Exception )
                {
                    lista = null;
                    cn.Close();
                    
                }
            }
            return lista;
        }
        //-----------------------------------------------------------------------------------------------
        //-------------------------------Guardar las Categorias------------------------------------------
        public int guardarCategoria(CategoriaCLS oCategoriaCLS)
        {
            int rta = 0;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarCategoria", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidcategoria", oCategoriaCLS.iidcategoria);
                        cmd.Parameters.AddWithValue("@nombre", oCategoriaCLS.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", oCategoriaCLS.descripcion);
                        cmd.Parameters.AddWithValue("@foto", oCategoriaCLS.foto);
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
