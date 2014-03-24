﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keramatian.Repository;

namespace Keramatian.Areas.en.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ITopBannerRepository _topBannerRepository;

        public HomeController(IBannerRepository bannerRepository, ICatalogRepository catalogRepository, IAnnouncementRepository announcementRepository, ITopBannerRepository topBannerRepository)
        {
            _bannerRepository = bannerRepository;
            _catalogRepository = catalogRepository;
            _announcementRepository = announcementRepository;
            _topBannerRepository = topBannerRepository;
        }

        [AllowAnonymousAttribute]
        public ActionResult Index()
        {

            ViewBag.TopBanner = _topBannerRepository.GetOne();
            ViewBag.Announcements = _announcementRepository.All();
            return View(_bannerRepository.All());
        }
        [AllowAnonymousAttribute]
        public ActionResult About()
        {


            return View();
        }
        [AllowAnonymousAttribute]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymousAttribute]
        public ActionResult GetPdf()
        {
            return _catalogRepository.GetFirst() != null
                       ? (ActionResult)
                         File(_catalogRepository.GetFirst().FilePath, "application/pdf",
                              Server.UrlEncode(_catalogRepository.GetFirst().FilePath))
                       : RedirectToAction("Index");
        }

    }
}
