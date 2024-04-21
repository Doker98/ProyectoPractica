using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
using System.IO;

namespace CapaTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idproducto = 0)
        {
            Producto oProducto = new Producto();
            bool conversion;

            oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == idproducto).FirstOrDefault();

            if (oProducto != null)
            {
                oProducto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen), out conversion);
                
            }


            return View(oProducto);
        }

        [HttpGet]

        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            lista = new CN_Categoria().Listar();

            return Json(new { data = lista},JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public JsonResult ListarProductos(int idcategoria)
        {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            lista = new CN_Producto().Listar().Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oCategoria = p.oCategoria,
                Precio = p.Precio,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen),out conversion),
                Activo = p.Activo,
            }).Where(p=>
            p.oCategoria.IdCategoria == (idcategoria == 0 ? p.oCategoria.IdCategoria : idcategoria) &&
            p.Activo == true   
            ).ToList();

            var jsonresult = Json(new {data =  lista},JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;

        }


        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {

            Carrito carrito = Session["IdCarrito"] as Carrito;
            int idcarrito = 0; // Valor por defecto o manejo de error

            if (carrito != null)
            {
                idcarrito = carrito.IdCarrito;
            }

            bool existe = new CN_Carrito().ExisteCarrito(idcarrito, idproducto);

            bool respuesta = false;

            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el Carrito";
            }
            else
            {
                respuesta= new CN_Carrito().OperacionCarrito(idcarrito, idproducto,true, out mensaje);
            }

            return Json (new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]

        public JsonResult CantidadEnCarrito()
        {
            Carrito carrito = Session["IdCarrito"] as Carrito;
            int idcarrito = 0; // Valor por defecto o manejo de error

            if (carrito != null)
            {
                idcarrito = carrito.IdCarrito;
            }

            int cantidad = new CN_Carrito().CantidadEnCarrito(idcarrito);
            return Json (new { cantidad = cantidad}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult ListarProductosCarrito()
        {
            Carrito carrito = Session["IdCarrito"] as Carrito;
            int idcliente = 0;

            List<Carrito_Producto> oLista = new List<Carrito_Producto>();

            bool conversion;

            oLista = new CN_Carrito().ListarProducto(idcliente).Select(oc=> new Carrito_Producto()
            {
                oProducto = new Producto()
                {
                    IdProducto = oc.oProducto.IdProducto,
                    Nombre = oc.oProducto.Nombre,
                    Precio = oc.oProducto.Precio,
                    
                },
                Cantidad = oc.Cantidad


            }).ToList();

            return Json (new {data = oLista}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {

            Carrito carrito = Session["IdCarrito"] as Carrito;
            int idcarrito = 0; // Valor por defecto o manejo de error

            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = new CN_Carrito().OperacionCarrito(idcarrito, idproducto, true, out mensaje);


            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]

        public JsonResult EliminarCarrito(int idproducto) 
        {
            Carrito carrito = Session["IdCarrito"] as Carrito;
            int idcarrito = 0; // Valor por defecto o manejo de error

            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = new CN_Carrito().EliminarCarrito(idcarrito, idproducto);

            return Json(new {respuesta = respuesta,mensaje = mensaje}, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Carrito() { 
            return View();

        }

    }
}