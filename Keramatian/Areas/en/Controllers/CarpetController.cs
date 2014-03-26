using System.Linq;
using System.Web.Mvc;
using Keramatian.Models;
using Keramatian.Repository;
using PagedList;

namespace Keramatian.Areas.en.Controllers
{
    public class CarpetController : Controller
    {

        private readonly ICarpetRepository _carpetRepository;
        private readonly IDesignRepository _designRepository;
        public CarpetController(ICarpetRepository carpetRepository, IDesignRepository designRepository)
        {
            _carpetRepository = carpetRepository;
            _designRepository = designRepository;
        }

        [HttpGet]
        [AllowAnonymousAttribute]
        public ActionResult Gallery(int? page, string designName)
        {
            var totalCarpetCount = _carpetRepository.GetDesignCount();
            var pageIndex = (page ?? 1) - 1;
            var pageSize = 1;
            var carpets = _carpetRepository.GetCarpets(pageIndex);
            var carpetsAsIPagedList = new StaticPagedList<Carpet>(carpets, pageIndex + 1, pageSize, totalCarpetCount);
            ViewBag.OnePageOfcarpets = carpetsAsIPagedList;
            ViewBag.Design = (_designRepository.GetDesignWithPriority(pageIndex));
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
        // for SEO
        [AllowAnonymousAttribute]
        public string GetDesignName(int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            return (_designRepository.GetDesignWithPriority(pageIndex)).Name;
        }

    }
}
