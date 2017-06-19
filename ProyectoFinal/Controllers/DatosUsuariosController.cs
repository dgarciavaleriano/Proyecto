using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class DatosUsuariosController : Controller
    {
        // GET: DatosUsuarios
        public ActionResult Index(int id)
        {
            var modelo = DAO.MapeadoDB.RecogerDatosUsuario(id);
            modelo.Identificado = false;
            modelo.Fans.SerFan = false;
            modelo.Fans.NoFan = false;

            if (Session["id"] != null)
            {
                if ((int)Session["id"] > 0)
                {
                    modelo.Fans.SerFan = true;
                    foreach (var fan in modelo.Fans.Fans)
                    {
                        if (fan.IdFan == (int)Session["id"])
                        {
                            modelo.Fans.SerFan = false;
                            modelo.Fans.NoFan = true;
                            break;
                        }
                    }

                    if ((int)Session["id"] == modelo.Id)
                    {
                        modelo.Identificado = true;
                    }
                }
            }
            else
            {
                Session["id"] = -1;
            }

            return View("~/Views/DatosUsuarios/Usuarios.cshtml", modelo);
        }

        public ActionResult InsertarFan(int idIdolatrado, int idFan)
        {
            var resultado = DAO.CrudDB.InsertarFan(idIdolatrado, idFan);

            return RedirectToAction("Index", "DatosUsuarios", new { id = idIdolatrado });
        }

        public ActionResult BorrarFan(int idIdolatrado, int idFan)
        {
            var resultado = DAO.CrudDB.BorrarFan(idIdolatrado, idFan);

            return RedirectToAction("Index", "DatosUsuarios", new { id = idIdolatrado });
        }

        public ActionResult Comentar(string comentario, int remitente, int destinatario, DateTime hora)
        {
            var resultado = DAO.CrudDB.ComentarDB(comentario, remitente, destinatario, hora);

            return RedirectToAction("Index", "DatosUsuarios", new { id = destinatario });
        }

        public ActionResult EditarComentario(string comentario, int idComentario, DateTime hora, int id)
        {
            var resultado = DAO.CrudDB.ModificarComentarioDB(comentario, idComentario, hora);

            return RedirectToAction("Index", "DatosUsuarios", new { id = id });
        }

        public ActionResult EliminarComentario(int idComentario, int id)
        {
            var resultado = DAO.CrudDB.BorrarComentario(idComentario);

            return RedirectToAction("Index", "DatosUsuarios", new { id = id });
        }
    }
}