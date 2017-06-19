using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class ComentarioModelo
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public int Remitente { get; set; }
        public string NombreRemitente { get; set; }
        public int Destinatario { get; set; }
        public DateTime Hora { get; set; }
    }
}
