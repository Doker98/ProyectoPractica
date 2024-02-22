using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Configuration;

namespace CapaAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        

        [Route("Mantenedor/Usuarios")]
        public ActionResult Usuarios()
        {
            
            return View();
        }

        public ActionResult Categorias()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }

        [Route("Mantenedor")]
        public ActionResult Index()
        {
            
            return View();
        }

        //++++++++++++++Categorias+++++++++++++++++++
        #region Categoria
        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(string objeto, HttpPostedFileBase archivoImagen)
        {

            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Categoria oCategoria = new Categoria();
            oCategoria = JsonConvert.DeserializeObject<Categoria>(objeto);


            if (oCategoria.IdCategoria == 0)
            {

                oCategoria.FechaRegistro = DateTime.Now;


                int idCategoriaGenerado = new CN_Categoria().Registrar(oCategoria, out mensaje);

                if (idCategoriaGenerado != 0)
                {
                    oCategoria.IdCategoria = idCategoriaGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                oCategoria.FechaActualizacion = DateTime.Now;
                operacion_exitosa = new CN_Categoria().Editar(oCategoria, out mensaje);
            }

            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"] + "\\Categorias";
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oCategoria.IdCategoria.ToString(), extension);


                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {
                        oCategoria.RutaImagen = nombre_imagen;
                        bool rspta = new CN_Categoria().GuardarDatosImagen(oCategoria, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }
                }

            }

            return Json(new { operacion_exitosa = operacion_exitosa, mensaje = mensaje, idGenerado = oCategoria.IdCategoria, rutaImagen= oCategoria.RutaImagen, fechaRegistro = oCategoria.FechaRegistro, fechaActualizacion = oCategoria.FechaActualizacion }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult ImagenCategoria(int id)
        {
            bool conversion;
            Categoria ocategoria = new CN_Categoria().Listar().Where(p => p.IdCategoria == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(ocategoria.RutaImagen), out conversion);

            return Json(new { conversion = conversion, textoBase64 = textoBase64, extension = Path.GetExtension(ocategoria.RutaImagen) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }
        #endregion Categoria
        //++++++++++++++Producto+++++++++++++++++++++
        #region Producto
        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
         {
            
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);
      
            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto,System.Globalization.NumberStyles.AllowDecimalPoint,new CultureInfo("es-CL"),out precio))
            {
                oProducto.Precio = precio;

            }
            else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##"},JsonRequestBehavior.AllowGet);
            }



            if (oProducto.IdProducto == 0)
            {

                oProducto.FechaRegistro = DateTime.Now;


                int idProductoGenerado = new CN_Producto().Registrar(oProducto, out mensaje);

                if(idProductoGenerado != 0)
                {
                    oProducto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                oProducto.FechaActualizacion = DateTime.Now;
                operacion_exitosa = new CN_Producto().Editar(oProducto, out mensaje);
            }

            if (operacion_exitosa)
            {
                if(archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"] + "\\Productos";
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(), extension); 


                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar,nombre_imagen));
                    }
                    catch(Exception ex) 
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {
                        oProducto.RutaImagen = ruta_guardar + "\\" + nombre_imagen;
                        bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }
                }

            }

            return Json(new { operacion_exitosa = operacion_exitosa, mensaje = mensaje,idGenerado = oProducto.IdProducto, fechaRegistro = oProducto.FechaRegistro, fechaActualizacion = oProducto.FechaActualizacion }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]

        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oproducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oproducto.RutaImagen),out conversion);

            return Json(new {conversion = conversion, textoBase64 = textoBase64, extension= Path.GetExtension(oproducto.RutaImagen)},JsonRequestBehavior.AllowGet);
        }


        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        #endregion Producto
    }
}