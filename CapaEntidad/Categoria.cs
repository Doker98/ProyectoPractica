using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string RutaImagen { get; set; }

        [JsonProperty("FechaRegistro")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime FechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime FechaActualizacion { get; set; }
        public string Base64 { get; set; }
        public string Extension { get; set; }

    }
}
