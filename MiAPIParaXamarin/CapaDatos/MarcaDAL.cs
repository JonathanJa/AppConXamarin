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
    public class MarcaDAL
    {
        public List<MarcaCLS> listarMarca()
        {
            List<MarcaCLS> lista = null;
            string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspListarMarca", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<MarcaCLS>();
                            MarcaCLS oMarcaCLS;
                            while (drd.Read())
                            {
                                oMarcaCLS = new MarcaCLS();
                                oMarcaCLS.iidmarca = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oMarcaCLS.nombre = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                lista.Add(oMarcaCLS);
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
