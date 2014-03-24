using System.Data;
using System.Linq;
using System.Web.Mvc;
using Keramatian.Models;
using Keramatian.Infrastructure;
using Keramatian.Repository;

namespace Keramatian.Controllers
{
    public class BackgroundColorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundColorRepository _backgroundColorRepository;

        public BackgroundColorController(IUnitOfWork unitOfWork, IBackgroundColorRepository backgroundColorRepository)
        {
            _unitOfWork = unitOfWork;
            _backgroundColorRepository = backgroundColorRepository;
        }

        //
        // GET: /BackgroundColor/

        public ActionResult Index()
        {
            return View(_backgroundColorRepository.All());
        }

        //
        // GET: /BackgroundColor/Details/5

        public ActionResult Details(int id = 0)
        {
            BackgroundColor backgroundcolor = _backgroundColorRepository.ById(id);
            if (backgroundcolor == null)
            {
                return HttpNotFound();
            }
            return View(backgroundcolor);
        }

        //
        // GET: /BackgroundColor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BackgroundColor/Create

        [HttpPost]
        public ActionResult Create(BackgroundColor backgroundcolor)
        {
            if (ModelState.IsValid)
            {
                _backgroundColorRepository.Add(backgroundcolor);
               _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(backgroundcolor);
        }

        //
        // GET: /BackgroundColor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var backgroundcolor = _backgroundColorRepository.ById(id);
            if (backgroundcolor == null)
            {
                return HttpNotFound();
            }
            return View(backgroundcolor);
        }

        //
        // POST: /BackgroundColor/Edit/5

        [HttpPost]
        public ActionResult Edit(BackgroundColor backgroundcolor)
        {
            if (ModelState.IsValid)
            {
               _backgroundColorRepository.Update(backgroundcolor);
               _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(backgroundcolor);
        }

        //
        // GET: /BackgroundColor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var backgroundcolor = _backgroundColorRepository.ById(id);
            if (backgroundcolor == null)
            {
                return HttpNotFound();
            }
            return View(backgroundcolor);
        }

        //
        // POST: /BackgroundColor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var backgroundcolor = _backgroundColorRepository.ById(id);
            _backgroundColorRepository.Delete(backgroundcolor);
           _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }
    }
}