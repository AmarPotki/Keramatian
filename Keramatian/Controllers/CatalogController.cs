using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keramatian.Infrastructure;
using Keramatian.Models;
using Keramatian.Infrastructure.DataAccess;
using Keramatian.Repository;

namespace Keramatian.Controllers
{
    public class CatalogController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICatalogRepository _catalogRepository;

        public CatalogController(IUnitOfWork unitOfWork, ICatalogRepository catalogRepository)
        {
            _unitOfWork = unitOfWork;
            _catalogRepository = catalogRepository;
        }

        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            return View(_catalogRepository.All());
        }

        //
        // GET: /Catalog/Details/5

        public ActionResult Details(int id = 0)
        {
            var catalog = _catalogRepository.ById(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        //
        // GET: /Catalog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Catalog/Create

        [HttpPost]
        public ActionResult Create(Catalog catalog, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/Banners/" + file.FileName));

                    catalog.FilePath = "/Content/Images/Banners/" + file.FileName;

                }
                _catalogRepository.Add(catalog);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(catalog);
        }

        //
        // GET: /Catalog/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var catalog = _catalogRepository.ById(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        //
        // POST: /Catalog/Edit/5

        [HttpPost]
        public ActionResult Edit(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _catalogRepository.Update(catalog);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(catalog);
        }

        //
        // GET: /Catalog/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var catalog = _catalogRepository.ById(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        //
        // POST: /Catalog/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var catalog = _catalogRepository.ById(id);
            _catalogRepository.Delete(catalog);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }


    }
}