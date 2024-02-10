﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Reporte
    {
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
                                //TotalVenta= Convert.ToInt32(dr["TotalVenta"]),
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