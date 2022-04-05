using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VentaDAL:CadenaDAL
    {
        public int guardarVenta(CompraCLS oCompraCLS)
        {
            int rpta = 0;
            VentasCLS oVentaCLS = oCompraCLS.oVentasCLS;
            List<DetalleVentaCLS> lista = oCompraCLS.listadetalles;
            //int iidAutogenerado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    cn.Open();
                    using (SqlTransaction transaccion = cn.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand("uspInsertarVentas", cn, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@iidusuario", oVentaCLS.iidusuario);
                            cmd.Parameters.AddWithValue("@total", oVentaCLS.total);
                            SqlParameter parametro = null;
                            parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                            parametro.Direction = ParameterDirection.ReturnValue;
                            rpta = cmd.ExecuteNonQuery();
                            oVentaCLS.iidventa = (int)parametro.Value;


                        }
                        foreach (DetalleVentaCLS oDetalleVentaCLS in lista)
                        {
                            using (SqlCommand cmd = new SqlCommand("uspInsertarDetalleVenta", cn, transaccion))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@iidventa", oVentaCLS.iidventa);
                                cmd.Parameters.AddWithValue("@iidproducto", oDetalleVentaCLS.iidproducto);
                                cmd.Parameters.AddWithValue("@cantidad", oDetalleVentaCLS.cantidad);
                                cmd.Parameters.AddWithValue("@precio", oDetalleVentaCLS.precio);
                                rpta = cmd.ExecuteNonQuery();
                                if (rpta == 0)
                                {
                                    transaccion.Rollback();
                                    return 0;
                                }
                            }
                            using (SqlCommand cmd = new SqlCommand("uspDisminuirStock", cn, transaccion))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@iidproducto", oDetalleVentaCLS.iidproducto);
                                cmd.Parameters.AddWithValue("@cantidad", oDetalleVentaCLS.cantidad);
                                rpta = cmd.ExecuteNonQuery();
                                if (rpta == 0)
                                {
                                    transaccion.Rollback();
                                    return 0;
                                }
                            }

                        }

                        transaccion.Commit();
                        rpta = 1;
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = 0;
            }


            return rpta;
        }



        //public int guardarVenta(CompraCLS oCompraCLS)
        //{
        //    int rpta = 0;
        //    VentasCLS oVentaCLS = oCompraCLS.oVentasCLS;
        //    List<DetalleVentaCLS> lista = oCompraCLS.listadetalles;
        //    using (SqlConnection cn = new SqlConnection(cadena))
        //    {
        //        try 
        //        {
        //            cn.Open();
        //            using (SqlTransaction transaction = cn.BeginTransaction())
        //            {
        //                using (SqlCommand cmd = new SqlCommand("uspInsertarVentas", cn, transaction))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@iidusuario", oVentaCLS.iidusuario);
        //                    cmd.Parameters.AddWithValue("@total", oVentaCLS.total);
        //                    SqlParameter parametro = null;
        //                    parametro = cmd.Parameters.Add("@@identity", SqlDbType.Int);
        //                    parametro.Direction = ParameterDirection.ReturnValue;
        //                    rpta = cmd.ExecuteNonQuery();
        //                    oVentaCLS.iidventa = (int)parametro.Value;

        //                }

        //                foreach(DetalleVentaCLS oDetalleVentasCLS in lista)
        //                {
        //                    using (SqlCommand cmd = new SqlCommand("uspInsertarDetalleVenta", cn, transaction))
        //                    {
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.AddWithValue("@iidusuario", oVentaCLS.iidusuario);
        //                        cmd.Parameters.AddWithValue("@iidproducto",oDetalleVentasCLS.iidproducto);
        //                        cmd.Parameters.AddWithValue("@cantidad", oDetalleVentasCLS.cantidad);
        //                        cmd.Parameters.AddWithValue("@precio", oDetalleVentasCLS.precio);
        //                        rpta = cmd.ExecuteNonQuery();
        //                        if(rpta == 0)
        //                        {
        //                            transaction.Rollback();
        //                            return 0;
        //                        }

        //                    }

        //                    using (SqlCommand cmd = new SqlCommand("uspDisminuirStock", cn, transaction))
        //                    {
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.AddWithValue("@iidproducto", oDetalleVentasCLS.iidproducto);
        //                        cmd.Parameters.AddWithValue("@cantidad", oDetalleVentasCLS.cantidad);
        //                        rpta = cmd.ExecuteNonQuery();
        //                        if (rpta == 0)
        //                        {
        //                            transaction.Rollback();
        //                            return 0;
        //                        }
        //                    }


        //                }

        //                transaction.Commit();
        //                rpta = 1;
        //                cn.Close();
        //            }


        //        }
        //        catch (Exception)
        //        {
        //            rpta = 0;
        //            cn.Close();
        //        }
        //    }
        //        return rpta;
        //}

    }
}
