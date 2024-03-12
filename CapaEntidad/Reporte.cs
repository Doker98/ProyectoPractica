using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reporte
    {
        public int IdVenta { get; set; }
        public string Fecha_Venta { get; set; }
        public string Cliente { get; set; }
        public string Rut_Cliente { get; set; }
        public string Email { get; set; }
        public string Descripcion_compra { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
        public string Transaction_id { get; set; }
    }
}
