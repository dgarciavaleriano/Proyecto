using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class DatosFans
    {
        public bool SerFan { get; set; }
        public bool NoFan { get; set; }
        public List<FanModelo> Fans { get; set; }
        public List<string> NombresFans { get; set; }
        public int NumeroFans { get; set; }
    }
}