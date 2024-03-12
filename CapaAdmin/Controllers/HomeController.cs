using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;
using Newtonsoft.Json;

namespace CapaAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario>oLista = new List<Usuario>();

            oLista = new CN_Usuarios().Listar();

            return Json(new { data = oLista },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;


            if (objeto.IdUsuario == 0)
            {

                objeto.FechaRegistro = DateTime.Now;
                
                resultado = new CN_Usuarios().Registrar(objeto,out mensaje);
            }
            else
            {
                objeto.FechaActualizacion = DateTime.Now;
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);
            }

            return Json(new {resultado = resultado, mensaje = mensaje, fechaRegistro = objeto.FechaRegistro, fechaActualizacion = objeto.FechaActualizacion }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }



        [HttpGet]

        public JsonResult ListaReporte(string fechainicio, string fechafin)
        {
            List<Reporte> oLista = new List<Reporte>();




            oLista = new CN_Reporte().Ventas(fechainicio,fechafin);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }





        [HttpGet]

        public JsonResult VistaDashBoard()
        {
            DashBoard objeto = new CN_Reporte().VerDashBoard();

            return Json(new { resultado = objeto}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public FileResult ExportarVenta(string fechainicio, string fechafin)
        {
            List<Reporte> oLista =new List<Reporte>();
            oLista = new CN_Reporte().Ventas(fechainicio, fechafin);

            DataTable dt = new DataTable();

            
            dt.Columns.Add("IdVenta", typeof(int));
            dt.Columns.Add("Fecha Venta", typeof(DateTime));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Rut Cliente", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Descripcion Compra", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Monto", typeof(decimal));
            dt.Columns.Add("Id Transacción", typeof(string));


            foreach (Reporte rp in oLista)
            {
                dt.Rows.Add(new object[]
                {
                    rp.IdVenta,
                    rp.Fecha_Venta,
                    rp.Cliente,
                    rp.Rut_Cliente,
                    rp.Email,
                    rp.Descripcion_compra,
                    rp.Cantidad,
                    rp.Monto,
                    rp.Transaction_id
                });
            }

            dt.TableName = "Datos";


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using(MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }

            }

        }




    }
}