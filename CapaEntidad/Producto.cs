using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal Precio { get; set; }
        public string PrecioTexto { get; set; }
        public string RutaImagen { get; set; }
        public bool Activo { get; set; }

        [JsonProperty("FechaRegistro")]
        [JsonConverter(typeof(CustomDateTimeConverter))] 
        public DateTime FechaRegistro { get; set; }
        [JsonProperty("FechaActualizacion")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime FechaActualizacion { get; set; }
        public string Base64 { get; set; }
        public string Extension { get; set; }
        public int IdCategoria { get; set; }
    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
        }
    }
}
