using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(Session["id"] != null)
            {
                if ((int)Session["id"] <= 0)
                {
                    Session["id"] = -1;
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }
    }
}