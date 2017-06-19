using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class ModeloGenerico
    {
        public int Id { get; set; }
        public bool Identificado { get; set; }
        public Enum.TipoUsuario Tipo { get; set; }
        public List<Enum.Estilo> Estilos { get; set; }
        public List<ComentarioModelo> Comentarios { get; set; }
        public List<BuscadorModelo> Buscados { get; set; }
        public List<CancionModelo> Canciones { get; set; }
        public List<FotoModelo> Fotos { get; set; }
        public DatosFans Fans { get; set; }
        public DatosGenerales Generales { get; set; }

        public ModeloGenerico()
        {
            Fans = new DatosFans();
            Generales = new DatosGenerales();
        }
    }
}
