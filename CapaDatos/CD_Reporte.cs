using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<Reporte> Ventas(string fechainicio, string fechafin)
        {

            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                 

                    SqlCommand cmd = new SqlCommand("dbo.sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.CommandType = CommandType.StoredProcedure;

             
                    
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Reporte()
                                {
                                    IdVenta = Convert.ToInt32(dr["Idventa"]),
                                    Fecha_Venta = dr["Fecha_Venta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Rut_Cliente = dr["Rut_Cliente"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Descripcion_compra = dr["Descripcion_compra"].ToString(),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    Monto = Convert.ToDecimal(dr["Monto"]),
                                    Transaction_id = dr["Transaction_id"].ToString(),
                                }
                                );


                        }
                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                lista = new List<Reporte>();



            }


            return lista;





        }


        public DashBoard VerDashBoard()
        {

            DashBoard objeto = new DashBoard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new DashBoard()
                            {
                                //Totalrecaudado= Convert.ToInt32(dr["Totalrecaudado"]),
                                TotalVenta= Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto= Convert.ToInt32(dr["TotalProducto"]),
                            };
                        }
                    }


                }

            }
            catch
            {

                objeto = new DashBoard();


            }

            return objeto;
        }



    }
}
