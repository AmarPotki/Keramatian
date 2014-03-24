using System.Web.Mvc;
using Keramatian.Repository;

namespace Keramatian.Areas.en.Controllers
{
    public class AnnouncementController : Controller
    {
        
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementController( IAnnouncementRepository announcementRepository)
        {
          
            _announcementRepository = announcementRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymousAttribute]
        public ActionResult Details(int id = 0)
        {
            var announcement = _announcementRepository.ById(id);
            if (announcement == null)
            {
                return HttpNotFound("آیتم مورد نظر یافت نشد");
            }
            return View(announcement);
        }

    }
}
