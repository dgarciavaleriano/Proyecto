using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models.Login
{
    public class SuscripcionPeticionModelo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public Models.Enum.TipoUsuario Tipo { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Website { get; set; }
        public string Direccion { get; set; }
        public Models.Enum.Estilo Estilo { get; set; }
    }
}
