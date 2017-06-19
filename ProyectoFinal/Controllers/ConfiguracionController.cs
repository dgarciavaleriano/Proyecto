using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: Configuracion
        public ActionResult Index(int id, Models.DatosGenerales peticion)
        {
            var resultado = false;
            if (peticion.Nombre != null)
            {
                resultado = DAO.CrudDB.ModificarDB(peticion, id);
            }

            var datos = DAO.MapeadoDB.RecogerDatosUsuario(id);
            datos.Estilos = DAO.MapeadoDB.RecogerEstilos();
            if (resultado)
                return RedirectToAction("Index", "DatosUsuarios", new { id = datos.Id });
            return View("~/Views/Configuracion/Configuracion.cshtml", datos);
        }

        public ActionResult Baja(int id)
        {
            var resultado = DAO.CrudDB.BorrarDB(id);

            return RedirectToAction("Index", "Login", new { borrado = true });
        }

        [HttpPost]
        public ActionResult SubirAudio(HttpPostedFileBase file, int id, string nombre)
        {
            if (file != null)
            {
                var ruta = "/Audio/" + file.FileName;

                var resultado = DAO.CrudDB.InsertarCancionDB(ruta, id, nombre);

                file.SaveAs(Server.MapPath(ruta));
            }

            return RedirectToAction("Index", "Configuracion", new { id = id});
        }

        public ActionResult BorrarCancion(int idCancion, int id)
        {
            var resultado = DAO.CrudDB.BorrarCancion(idCancion);

            return RedirectToAction("Index", "Configuracion", new { id = id});
        }

        [HttpPost]
        public ActionResult SubirFoto(HttpPostedFileBase file, int id, string nombre, string descripcion2, bool? perfil)
        {
            if (perfil == null)
            {
                perfil = false;
            }
            if (file != null)
            {
                var ruta = "/Foto/" + file.FileName;

                var resultado = DAO.CrudDB.InsertarFotosDB(ruta, id, nombre, descripcion2, perfil);

                file.SaveAs(Server.MapPath(ruta));
            }

            return RedirectToAction("Index", "Configuracion", new { id = id });
        }

        public ActionResult CambiarFotoPerfil(int idCancion, int id)
        {
            var resultado = DAO.CrudDB.ModificarFotoPerfil(idCancion);

            return RedirectToAction("Index", "Configuracion", new { id = id });
        }

        public ActionResult BorrarFoto(int idCancion, int id)
        {
            var resultado = DAO.CrudDB.BorrarFoto(idCancion);

            return RedirectToAction("Index", "Configuracion", new { id = id });
        }
    }
}