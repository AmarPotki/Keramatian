using System;
using System.Web;
using System.Web.Mvc;
using Keramatian.Infrastructure;
using Keramatian.Models;
using Keramatian.Repository;

namespace Keramatian.Controllers
{
    public class TopBannerController : Controller
    {
        private readonly ITopBannerRepository _topBannerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TopBannerController(ITopBannerRepository topBannerRepository, IUnitOfWork unitOfWork)
        {
            _topBannerRepository = topBannerRepository;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /TopBanner/

        public ActionResult Index()
        {
            return View(_topBannerRepository.All());
        }

        //
        // GET: /TopBanner/Details/5

        public ActionResult Details(int id = 0)
        {
            var topbanner = _topBannerRepository.ById(id);
            if (topbanner == null)
            {
                return HttpNotFound();
            }
            return View(topbanner);
        }

        //
        // GET: /TopBanner/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TopBanner/Create

        [HttpPost]
        public ActionResult Create(TopBanner topbanner, HttpPostedFileBase file, HttpPostedFileBase enFile)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // var imageName = DateTime.Now.ToString(CultureInfo.InvariantCulture) +
                    //System.IO.Path.GetExtension(file.FileName);
                    var imageName = DateTime.Now.ToString("yyyMMddhhmm") + System.IO.Path.GetExtension(file.FileName);


                    file.SaveAs(HttpContext.Server.MapPath("~/Images/TopBanners/" + imageName));

                    topbanner.ImagePath = "/Images/TopBanners/" + imageName;
                    topbanner.ImageName = imageName;
                }
                if (enFile.ContentLength > 0)
                {
                    var imageName = DateTime.Now.ToString("en_yyyMMddhhmm") + System.IO.Path.GetExtension(enFile.FileName);
                    enFile.SaveAs(HttpContext.Server.MapPath("~/Images/TopBanners/" + imageName));
                    topbanner.EnImagePath = "/Images/TopBanners/" + imageName;

                }
                _topBannerRepository.Add(topbanner);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(topbanner);
        }

        //
        // GET: /TopBanner/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var topbanner = _topBannerRepository.ById(id);
            if (topbanner == null)
            {
                return HttpNotFound();
            }
            return View(topbanner);
        }

        //
        // POST: /TopBanner/Edit/5

        [HttpPost]
        public ActionResult Edit(TopBanner topbanner)
        {
            if (ModelState.IsValid)
            {
                _topBannerRepository.Update(topbanner);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(topbanner);
        }

        //
        // GET: /TopBanner/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var topbanner = _topBannerRepository.ById(id);
            if (topbanner == null)
            {
                return HttpNotFound();
            }
            return View(topbanner);
        }

        //
        // POST: /TopBanner/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var topbanner = _topBannerRepository.ById(id);
            _topBannerRepository.Delete(topbanner);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }


    }
}