using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class FotoModelo
    {
        public int Id { get; set; }
        public string Ruta { get; set; }
        public int Propietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Perfil { get; set; }
        public bool Portada { get; set; }
    }
}
