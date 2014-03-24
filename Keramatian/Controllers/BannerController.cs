using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keramatian.Infrastructure;
using Keramatian.Models;
using Keramatian.Infrastructure.DataAccess;
using Keramatian.Repository;

namespace Keramatian.Controllers
{
    public class BannerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBannerRepository _bannerRepository;

        public BannerController(IUnitOfWork unitOfWork, IBannerRepository bannerRepository)
        {
            _unitOfWork = unitOfWork;
            _bannerRepository = bannerRepository;
        }

        //
        // GET: /Banner/

        public ActionResult Index()
        {
            return View(_bannerRepository.All());
        }

        //
        // GET: /Banner/Details/5

        public ActionResult Details(int id = 0)
        {
            var banner = _bannerRepository.ById(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        //
        // GET: /Banner/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Banner/Create

        [HttpPost]
        public ActionResult Create(Banner banner, HttpPostedFileBase file, HttpPostedFileBase enFile)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // var imageName = DateTime.Now.ToString(CultureInfo.InvariantCulture) +
                    //System.IO.Path.GetExtension(file.FileName);
                    var imageName = DateTime.Now.ToString("yyyMMddhhmm") + System.IO.Path.GetExtension(file.FileName);


                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Banners/" + imageName));

                    banner.ImagePath = "/Images/Banners/" + imageName;
                    banner.ImageName = imageName;
                }
                if (enFile.ContentLength > 0)
                {
                    var imageName = DateTime.Now.ToString("en_yyyMMddhhmm") + System.IO.Path.GetExtension(enFile.FileName);
                    enFile.SaveAs(HttpContext.Server.MapPath("~/Images/Banners/" + imageName));
                    banner.EnImagePath = "/Images/Banners/" + imageName;

                }
                _bannerRepository.Add(banner);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        //
        // GET: /Banner/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var banner = _bannerRepository.ById(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        //
        // POST: /Banner/Edit/5

        [HttpPost]
        public ActionResult Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                _bannerRepository.Update(banner);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        //
        // GET: /Banner/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var banner = _bannerRepository.ById(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        //
        // POST: /Banner/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var banner = _bannerRepository.ById(id);
            _bannerRepository.Delete(banner);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //  db.Dispose();
            //base.Dispose(disposing);
        }
    }
}