using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keramatian.Models;
using Keramatian.Repository;
using PagedList;

namespace Keramatian.Areas.en.Controllers
{
    public class CarpetController : Controller
    {

        private readonly ICarpetRepository _carpetRepository;
        public CarpetController(ICarpetRepository carpetRepository)
        {
            _carpetRepository = carpetRepository;
        }

        [HttpGet]
        [AllowAnonymousAttribute]
        public ActionResult Gallery(int? page)
        {
            var totalCarpetCount = _carpetRepository.GetCount();
            var pageIndex = (page ?? 1) - 1;
            var pageSize = 6;
            var carpets = _carpetRepository.GetCarpets(page, pageSize);
            var carpetsAsIPagedList = new StaticPagedList<Carpet>(carpets, pageIndex + 1, pageSize, totalCarpetCount);
            ViewBag.OnePageOfcarpets = carpetsAsIPagedList;
            if (carpetsAsIPagedList.Count > 3)
            {
                ViewBag.OnePageOfcarpets1 = carpetsAsIPagedList.Take(3);
                ViewBag.OnePageOfcarpets2 = carpetsAsIPagedList.Skip(3);
            }
            else
            {
                ViewBag.OnePageOfcarpets1 = carpetsAsIPagedList;
            }


            return View();
        }

    }
}
