using System;
using System.Web;
using System.Web.Mvc;
using Keramatian.Models;
using Keramatian.Repository;
using Keramatian.Infrastructure;

namespace Keramatian.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementController(IUnitOfWork unitOfWork, IAnnouncementRepository announcementRepository)
        {
            _unitOfWork = unitOfWork;
            _announcementRepository = announcementRepository;
        }

        //
        // GET: /Announcement/

        public ActionResult Index()
        {
            return View(_announcementRepository.All());
        }

        //
        // GET: /Announcement/Details/5
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

        //
        // GET: /Announcement/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Announcement/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Announcement announcement, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // var imageName = DateTime.Now.ToString(CultureInfo.InvariantCulture) +
                    //System.IO.Path.GetExtension(file.FileName);
                    var imageName = DateTime.Now.ToString("ddyyyMMmmhh") + System.IO.Path.GetExtension(file.FileName);
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Announcement/" + imageName));
                    announcement.ImagePath = "/Images/Announcement/" + imageName;
                }
                _announcementRepository.Add(announcement);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(announcement);
        }

        //
        // GET: /Announcement/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var announcement = _announcementRepository.ById(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        //
        // POST: /Announcement/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _announcementRepository.Update(announcement);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        //
        // GET: /Announcement/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var announcement = _announcementRepository.ById(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        //
        // POST: /Announcement/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var announcement = _announcementRepository.ById(id);
            try
            {
                System.IO.File.Delete(HttpContext.Server.MapPath(announcement.ImagePath));
                _announcementRepository.Delete(announcement);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {

                _announcementRepository.Delete(announcement);
                _unitOfWork.Commit();
            }
           
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }
    }
}