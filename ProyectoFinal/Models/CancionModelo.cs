using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class CancionModelo
    {
        public int Id { get; set; }
        public string Ruta { get; set; }
        public int Propietario { get; set; }
        public string Nombre { get; set; }
    }
}
