using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using exoCatalogueProduit.Models;
using System.Data.Entity.Migrations;

namespace exoCatalogueProduit.Controllers
{
    public class CategorieController : Controller
    {
        CATALOGUE_Entities db = new CATALOGUE_Entities();
        // GET: Categorie
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ajouterCategorie()
        {
            try
            {
                ViewBag.listCategorie = db.CAT_CATEGORIE.ToList();
                
                return View();
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ajouterCategorie(CAT_CATEGORIE categorie)//ajouter categorie
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categorie.DATE_SAISIE = DateTime.Now;
                    db.CAT_CATEGORIE.Add(categorie);
                    db.SaveChanges();
                }
                return RedirectToAction("ajouterCategorie");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        public ActionResult SupprimerCategorie(int id)
        {
            try
            {
                CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id);//recherche de la categorie
                if (categorie != null)
                {
                    db.CAT_CATEGORIE.Remove(categorie);//supprimer la categorie
                    db.SaveChanges(); //
                }
                return RedirectToAction("ajouterCategorie");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        public ActionResult ModifierCategorie(int id)
        {
            try
            {
                ViewBag.listCategorie = db.CAT_CATEGORIE.ToList();
                CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id);//recherche de la categorie
                if (categorie != null)
                {
                    return View("ajouterCategorie",categorie);
                }
                
                return RedirectToAction("ajouterCategorie");
            }
            catch(Exception e)
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ModifierCategorie(CAT_CATEGORIE categorie)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    db.Entry(categorie).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("ajouterCategorie");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
    }
}