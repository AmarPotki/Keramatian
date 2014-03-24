using System.Data;
using System.Web.Mvc;
using Keramatian.Infrastructure;
using Keramatian.Models;
using Keramatian.Repository;

namespace Keramatian.Controllers
{
    public class PlainController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlainRepository _plainRepository;

        public PlainController(IUnitOfWork unitOfWork, IPlainRepository plainRepository)
        {
            _unitOfWork = unitOfWork;
            _plainRepository = plainRepository;
        }

        //
        // GET: /Plain/

        public ActionResult Index()
        {
            return View(_plainRepository.All());
        }

        //
        // GET: /Plain/Details/5

        public ActionResult Details(int id = 0)
        {
            var plain = _plainRepository.ById(id);
            if (plain == null)
            {
                return HttpNotFound();
            }
            return View(plain);
        }

        //
        // GET: /Plain/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Plain/Create

        [HttpPost]
        public ActionResult Create(Plain plain)
        {
            if (ModelState.IsValid)
            {
                _plainRepository.Add(plain);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(plain);
        }

        //
        // GET: /Plain/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var plain = _plainRepository.ById(id);
            if (plain == null)
            {
                return HttpNotFound();
            }
            return View(plain);
        }

        //
        // POST: /Plain/Edit/5

        [HttpPost]
        public ActionResult Edit(Plain plain)
        {
            if (ModelState.IsValid)
            {
               _plainRepository.Update(plain);
              _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(plain);
        }

        //
        // GET: /Plain/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var plain = _plainRepository.ById(id);
            if (plain == null)
            {
                return HttpNotFound();
            }
            return View(plain);
        }

        //
        // POST: /Plain/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var plain = _plainRepository.ById(id);
           _plainRepository.Delete(plain);
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