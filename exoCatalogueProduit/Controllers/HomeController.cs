using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using exoCatalogueProduit.Models;

namespace exoCatalogueProduit.Controllers
{
    public class HomeController : Controller
    {
        CATALOGUE_Entities db = new CATALOGUE_Entities();
        
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.listCategorie = db.CAT_CATEGORIE.ToList().OrderBy(r => r.LIBELLE_CATEGORIE);//list de cat par ordre alph
            return View();
        }
    }
}