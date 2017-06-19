using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProyectoFinal.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(Models.Login.LoginPeticionModelo peticion, bool suscrito = false, bool borrado = false)
        {
            var modelo = new Models.Login.LoginModelo() { Suscrito = suscrito };

            if (borrado)
            {
                Session["id"] = null;
                Session["identificado"] = null;
                Session["nombre"] = null;
            }

            if (peticion != null)
            {
                if (peticion.Email != null)
                {
                    var identificado = DAO.MapeadoDB.Identificado(peticion.Email, peticion.Password);
                    if (identificado.Resultado)
                    {
                        Session["id"] = identificado.Id;
                        Session["identificado"] = true;
                        Session["nombre"] = identificado.Nombre;
                        return RedirectToAction("Index", "DatosUsuarios", new { id = identificado.Id });
                    }
                }

            }
            return View("~/Views/Login/Index.cshtml", modelo);
        }

        public ActionResult Suscripcion(Models.Enum.TipoUsuario? tipo)
        { 
            switch (tipo)
            {
                case Models.Enum.TipoUsuario.Banda:
                    var estilos = DAO.MapeadoDB.RecogerEstilos();
                    var modelBanda = new Models.Login.SuscripcionBandaModelo() { Estilos = estilos, Tipo = (Models.Enum.TipoUsuario)tipo };
                    return View("~/Views/Login/Suscripcion/SuscripcionBanda.cshtml", modelBanda);
                case Models.Enum.TipoUsuario.Manager:
                    var generalManager = new Models.DatosGenerales() { Estilo = Models.Enum.Estilo.SinEstilo };
                    var modelManager = new Models.ModeloGenerico() { Tipo = (Models.Enum.TipoUsuario)tipo, Generales = generalManager};
                    return View("~/Views/Login/Suscripcion/SuscripcionManager.cshtml", modelManager);
                case Models.Enum.TipoUsuario.Sala:
                    var generalSala = new Models.DatosGenerales() { Estilo = Models.Enum.Estilo.SinEstilo };
                    var modelSala = new Models.ModeloGenerico() { Tipo = (Models.Enum.TipoUsuario)tipo, Generales = generalSala};
                    return View ("~/Views/Login/Suscripcion/SuscripcionSala.cshtml", modelSala);
            }
            return View("~/Views/Login/Suscripcion/SuscripcionTipo.cshtml");
        }

        [HttpPost]
        public ActionResult SuscripcionDB(Models.Login.SuscripcionPeticionModelo peticion)
        {
            var resultado = DAO.CrudDB.InsertarDB(peticion);
            return RedirectToAction("Index", "Login", new { suscrito = resultado });
        }

        public ActionResult CerrarSesion()
        {
            Session["id"] = -1;
            Session["identificado"] = false;
            Session["nombre"] = string.Empty;
            return RedirectToAction("Index", "Login");
        }
    }
}
