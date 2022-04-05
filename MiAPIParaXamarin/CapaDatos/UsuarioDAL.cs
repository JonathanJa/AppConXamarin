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
    public class UsuarioDAL:CadenaDAL
    {
        public UsuarioCLS Login(string usuario,string contra)
        {
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspLogin", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contra", contra);
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {


                            while (drd.Read())
                            {

                                oUsuarioCLS.iidusuario = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oUsuarioCLS.iidtipousuario = drd.IsDBNull(1) ? 0 : drd.GetInt32(1);
                                
                            }
                            cn.Close();
                        }
                    }

                }
                catch (Exception)
                {

                    cn.Close();
                    oUsuarioCLS = null;
                }
            }
            return oUsuarioCLS;
        }
    }
}
