using System.Linq;
using System.Web.Mvc;
using Keramatian.Models;
using Keramatian.Repository;
using PagedList;

namespace Keramatian.Areas.en.Controllers
{
    public class LeatherCarpetController : Controller
    {
        private readonly ILeatherCarpetRepository _leatherCarpetRepository;
        public LeatherCarpetController(ILeatherCarpetRepository leatherCarpetRepository)
        {
            _leatherCarpetRepository = leatherCarpetRepository;
        }

        [HttpGet]
        [AllowAnonymousAttribute]
        public ActionResult Gallery(int? page)
        {
            var totalCarpetCount = _leatherCarpetRepository.GetCount();
            var pageIndex = (page ?? 1) - 1;
            var pageSize = 6;
            var leatherCarpets = _leatherCarpetRepository.GetLeatherCarpets(page, pageSize);
            var carpetsAsIPagedList = new StaticPagedList<LeatherCarpet>(leatherCarpets, pageIndex + 1, pageSize, totalCarpetCount);
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
