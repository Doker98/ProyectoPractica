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
        public Carrito oCarrito { get; set; }
        public int oIdCarrito { get; set; }
        public int oIdProducto { get; set; }
        
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }

    }
}
