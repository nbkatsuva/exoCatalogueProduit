using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using exoCatalogueProduit.Models;
using System.IO;

namespace exoCatalogueProduit.Controllers
{
    public class ProduitController : Controller
    {
        CATALOGUE_Entities db = new CATALOGUE_Entities();
        // GET: Produit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ajouterProduit()
        {
            try
            {
                ViewBag.listProduit = db.CAT_PRODUIT.ToList();
                ViewBag.listCategorie = db.CAT_CATEGORIE.ToList();
                return View();
            }catch(Exception e)
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ajouterProduit(CAT_PRODUIT produit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];//le nom de notre fichier
                        if(file!=null && file.ContentLength > 0)
                        {
                          var  fileName = Path.GetFileName(file.FileName);//recuperer le nom du fichier
                            var path = Path.Combine(Server.MapPath("~/Fichier"),fileName);//recuperer le chemin d acces de ou sera mis notre fichier
                            file.SaveAs(path);//enregistrer les touts
                            produit.IMAGE_PRODUIT = fileName;
                            produit.URL_IMAGE_PRODUIT = "/Fichier";
                        }
                    }
                    produit.DATE_SAISIE = DateTime.Now;
                    db.CAT_PRODUIT.Add(produit);
                    db.SaveChanges();
                }
                return RedirectToAction("ajouterProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        public ActionResult SupprimerProduit(int id)
        {
            try
            {
                CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id);//recherche de la categorie
                if (produit != null)
                {
                    db.CAT_PRODUIT.Remove(produit);//supprimer la categorie
                    db.SaveChanges(); //
                }
                return RedirectToAction("ajouterProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        public ActionResult ModifierProduit(int id)
        {
            try
            {
                ViewBag.listProduit = db.CAT_PRODUIT.ToList();
                ViewBag.listCategorie = db.CAT_CATEGORIE.ToList();
                CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id);//recherche de la categorie
                if (produit != null)
                {
                    return View("ajouterProduit", produit);
                }

                return RedirectToAction("ajouterProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ModifierProduit(CAT_PRODUIT produit)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Entry(produit).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("ajouterProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
    }
}