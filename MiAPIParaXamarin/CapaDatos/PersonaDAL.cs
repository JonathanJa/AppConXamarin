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
    public class PersonaDAL:CadenaDAL
    {
        public int guardarPersona(PersonaCLS oPersonaCLS)
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
                        using (SqlCommand cmd = new SqlCommand("uspGuardarPersona", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iidpersona", oPersonaCLS.iidpersona);
                            cmd.Parameters.AddWithValue("@nombre", oPersonaCLS.nombre);
                            cmd.Parameters.AddWithValue("@appaterno", oPersonaCLS.appaterno);
                            cmd.Parameters.AddWithValue("@apmaterno", oPersonaCLS.apmaterno);
                            cmd.Parameters.AddWithValue("@telefono", oPersonaCLS.telefono);
                            cmd.Parameters.AddWithValue("@correo", oPersonaCLS.correo);
                            cmd.Parameters.AddWithValue("@fechanac", oPersonaCLS.fechanacimientodate);
                            cmd.Parameters.AddWithValue("@btieneusuario", oPersonaCLS.isTieneusuario);
                            
                            if (oPersonaCLS.iidpersona == 0)
                            {
                                //Nuevo
                                parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                                parametro.Direction = ParameterDirection.ReturnValue;
                            }
                            rpta = cmd.ExecuteNonQuery();
                           
                        }
                        if (rpta == 1 && oPersonaCLS.iidpersona == 0)
                        {
                            iidAutogenerado = (int)parametro.Value;
                            oPersonaCLS.iidpersona = iidAutogenerado;
                        }
                        //crear el usuario es opcional
                        if(oPersonaCLS.isTieneusuario == true) {

                            using (SqlCommand cmd2 = new SqlCommand("uspGuardarUsuario", cn, transaccion))
                            {
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Parameters.AddWithValue("@iidusuario", oPersonaCLS.iidusuario);
                                cmd2.Parameters.AddWithValue("@nombreusuario", oPersonaCLS.nombreusuario);
                                cmd2.Parameters.AddWithValue("@contra", oPersonaCLS.contra);
                                cmd2.Parameters.AddWithValue("@iidpersona", oPersonaCLS.iidpersona);
                                cmd2.Parameters.AddWithValue("@iidtipousuario", oPersonaCLS.iidtipoUsuario);
                                rpta = cmd2.ExecuteNonQuery();
                            }


                        }
                        

                            //Esto ejecuta
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
        public PersonaCLS recuperarPersona(int iidpersona)
        {
            PersonaCLS oPersonaCLS = new PersonaCLS();
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarPersonas", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidpersona", iidpersona);
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            
                            
                            while (drd.Read())
                            {
                                
                                oPersonaCLS.iidpersona = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oPersonaCLS.nombre= drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oPersonaCLS.appaterno= drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oPersonaCLS.apmaterno = drd.IsDBNull(3) ? "" : drd.GetString(3);
                                oPersonaCLS.telefono = drd.IsDBNull(4) ? "" : drd.GetString(4);
                                oPersonaCLS.correo = drd.IsDBNull(5) ? "" : drd.GetString(5);
                                oPersonaCLS.fechanacimientodate = drd.IsDBNull(6) ? DateTime.Now : drd.GetDateTime(6);
                                oPersonaCLS.isTieneusuario = drd.IsDBNull(8) ? false : (drd.GetInt32(8) == 1? true:false);
                                oPersonaCLS.iidusuario = drd.IsDBNull(9) ? 0 : drd.GetInt32(9);
                                oPersonaCLS.nombreusuario = drd.IsDBNull(10) ? "" : drd.GetString(10);
                                oPersonaCLS.nombretipousuario = drd.IsDBNull(11) ? "" : drd.GetString(11);

                            }
                            cn.Close();
                        }
                    }

                }
                catch (Exception)
                {
                    
                    cn.Close();

                }
            }
            return oPersonaCLS;
        }
        public List<PersonaCLS> listarPersona()
        {
            List<PersonaCLS> lista = null;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    //abrimos la conexion 
                    cn.Open();
                    //definio el procedure
                    using (SqlCommand cmd = new SqlCommand("uspListarPersonas", cn))
                    {
                        //indico que era un procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            lista = new List<PersonaCLS>();
                            PersonaCLS oPersonaCLS;
                            while (drd.Read())
                            {
                                oPersonaCLS = new PersonaCLS();
                                oPersonaCLS.iidpersona = drd.IsDBNull(0) ? 0 : drd.GetInt32(0);
                                oPersonaCLS.nombrecompleto = drd.IsDBNull(1) ? "" : drd.GetString(1);
                                oPersonaCLS.fechanacimiento= drd.IsDBNull(2) ? "" : drd.GetString(2);
                                oPersonaCLS.telefono = drd.IsDBNull(3) ? "" : drd.GetString(3);
                                oPersonaCLS.tieneusuario = drd.IsDBNull(4) ? "" : (drd.GetInt32(4) == 0 ? "No" : "Si");
                                lista.Add(oPersonaCLS);
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
        public int eliminarPersona(int idpersona)
        {
            int rta = 0;
            //string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarPersonas", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iidpersona", idpersona);
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
