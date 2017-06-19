using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class BuscadorController : Controller
    {
        // GET: Buscador
        public ActionResult Index(string busqueda)
        {
            var usuarios = DAO.MapeadoDB.RecogerUsuarios();
            var modelo = new Models.ModeloGenerico();
            var encontrados = new List<Models.BuscadorModelo>();

            foreach (var usuario in usuarios)
            {
                if (usuario.Generales.Nombre.ToLower().Contains(busqueda.ToLower()))
                {
                    if (!encontrados.Contains(usuario))
                    {
                        encontrados.Add(DAO.MapeadoDB.ConversionGenericoBuscador(usuario));
                    }
                }
            }

            modelo.Buscados = encontrados;

            return View("~/Views/Buscador/Buscador.cshtml", modelo);
        }
    }
}