using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class DatosGenerales
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Direccion2 { get; set; }
        public string Website { get; set; }
        public string Sonido { get; set; }
        public string Perfil { get; set; }
        public int Ranking { get; set; }
        public Enum.Estilo Estilo { get; set; }
    }
}