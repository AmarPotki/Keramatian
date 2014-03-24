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
    public class SizeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISizeRepository _sizeRepository;

        public SizeController(IUnitOfWork unitOfWork, ISizeRepository sizeRepository)
        {
            _unitOfWork = unitOfWork;
            _sizeRepository = sizeRepository;
        }

        //
        // GET: /Size/

        public ActionResult Index()
        {
            return View(_sizeRepository.All());
        }

        //
        // GET: /Size/Details/5

        public ActionResult Details(int id = 0)
        {
            Size size = _sizeRepository.ById(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        //
        // GET: /Size/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Size/Create

        [HttpPost]
        public ActionResult Create(Size size)
        {
            if (ModelState.IsValid)
            {
                _sizeRepository.Add(size);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(size);
        }

        //
        // GET: /Size/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var size = _sizeRepository.ById(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        //
        // POST: /Size/Edit/5

        [HttpPost]
        public ActionResult Edit(Size size)
        {
            if (ModelState.IsValid)
            {
                _sizeRepository.Update(size);
               _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(size);
        }

        //
        // GET: /Size/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var size = _sizeRepository.ById(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        //
        // POST: /Size/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var size = _sizeRepository.ById(id);
            _sizeRepository.Delete(size);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
           // base.Dispose(disposing);
        }
    }
}