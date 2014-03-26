using System;
using System.Web;
using System.Web.Mvc;
using Keramatian.Infrastructure;
using Keramatian.Models;
using Keramatian.Repository;

namespace Keramatian.Controllers
{
    public class DesignController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDesignRepository _designRepository;
        public DesignController(IUnitOfWork unitOfWork, IDesignRepository designRepository)
        {
            _unitOfWork = unitOfWork;
            _designRepository = designRepository;
        }

        //
        // GET: /Design/

        public ActionResult Index()
        {
            return View(_designRepository.All());
        }

        //
        // GET: /Design/Details/5

        public ActionResult Details(int id = 0)
        {
            var design = _designRepository.ById(id);
            if (design == null)
            {
                return HttpNotFound();
            }
            return View(design);
        }

        //
        // GET: /Design/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Design/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Design design, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var imageName = DateTime.Now.ToString("ddyyyMMmmhh") + System.IO.Path.GetExtension(file.FileName);
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Design/" + imageName));
                    design.ImagePath = "/Images/Design/" + imageName;
                }
                _designRepository.Add(design);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(design);
        }

        //
        // GET: /Design/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var design = _designRepository.ById(id);

            if (design == null)
            {
                return HttpNotFound();
            }
            //  ViewBag.ImagePath = design.ImagePath;

            Session.Add("ImagePath", design.ImagePath);
            return View(design);
        }

        //
        // POST: /Design/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Design design, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (!string.IsNullOrEmpty(Session["ImagePath"].ToString()))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath(Session["ImagePath"].ToString()));
                        Session.Remove("ImagePath");
                    }

                    var imageName = DateTime.Now.ToString("ddyyyMMmmhh") + System.IO.Path.GetExtension(file.FileName);
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Design/" + imageName));
                    design.ImagePath = "/Images/Design/" + imageName;

                }
                _designRepository.Update(design);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(design);
        }

        //
        // GET: /Design/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var design = _designRepository.ById(id);
            if (design == null)
            {
                return HttpNotFound();
            }
            return View(design);
        }

        //
        // POST: /Design/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var design = _designRepository.ById(id);
            System.IO.File.Delete(HttpContext.Server.MapPath(design.ImagePath));
            _designRepository.Delete(design);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }


    }
}