﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Carrito
    {
        private CD_Carrito objCapaDato = new CD_Carrito();

        public bool ExisteCarrito(int idcarrito, int idproducto)
        {
            return objCapaDato.ExisteCarrito(idcarrito, idproducto);
        }

        public bool OperacionCarrito(int idcarrito, int idproducto, bool sumar, out string Mensaje)
        {
            return objCapaDato.OperacionCarrito(idcarrito, idproducto,sumar, out Mensaje);
        }

        public int CantidadEnCarrito(int idcarrito)
        {
            return objCapaDato.CantidadEnCarrito(idcarrito);
        }

        public List<Carrito_Producto> ListarProducto(int idcliente)
        {
            return objCapaDato.ListarProducto(idcliente);
        }

        public bool EliminarCarrito(int idcarrito, int idproducto)
        {
            return objCapaDato.EliminarCarrito(idcarrito, idproducto);
        }


    }
}
