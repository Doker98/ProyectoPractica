using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Carrito_Producto
    {
        public int IdCarritoProducto { get; set; }
        public Carrito oIdCarrito { get; set; }
        public Producto oIdProducto { get; set; }
        public int Cantidad { get; set; }

    }
}
