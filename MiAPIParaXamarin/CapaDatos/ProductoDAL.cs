using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ProductoDAL:CadenaDAL
    {


        public int guardarProducto(ProductoCLS oProductoCLS)
        {
            int rta = 0;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarProducto", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidproducto", oProductoCLS.iidproducto);
                        cmd.Parameters.AddWithValue("@nombre", oProductoCLS.nombre);
                        cmd.Parameters.AddWithValue("@precio", oProductoCLS.precio);
                        cmd.Parameters.AddWithValue("@iidcategoria", oProductoCLS.iidcategoria);
                        cmd.Parameters.AddWithValue("@stock", oProductoCLS.stock);
                        cmd.Parameters.AddWithValue("@iidmarca", oProductoCLS.iidmarca);
                        cmd.Parameters.AddWithValue("@foto", oProductoCLS.foto);
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

        public int eliminarProducto(int idProducto)
        {
            int rta = 0;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarProducto", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidproducto", idProducto);
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

        public List<ProductoCLS> listarProducto()
        {
            List<ProductoCLS> lista = null;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspListarProductos", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<ProductoCLS>();
                            ProductoCLS oProductoCLS;
                            while (drd.Read())
                            {
                                oProductoCLS = new ProductoCLS();
                                oProductoCLS.iidproducto= drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oProductoCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oProductoCLS.precio = drd.IsDBNull(2) ? 0 : drd.GetDecimal(2);
                                oProductoCLS.stock = drd.IsDBNull(3) ? 0 : drd.GetInt32(3);
                                oProductoCLS.nombrecategoria = drd.IsDBNull(4) ? "" : drd.GetString(4);
                                oProductoCLS.nombremarca = drd.IsDBNull(5) ? "" : drd.GetString(5);
                                oProductoCLS.foto = drd.IsDBNull(6) ? null : (byte[])drd.GetValue(6);
                                lista.Add(oProductoCLS);
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
    }
}
