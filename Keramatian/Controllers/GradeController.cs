using System.Web.Mvc;
using Keramatian.Models;
using Keramatian.Repository;
using Keramatian.Infrastructure;

namespace Keramatian.Controllers
{
    public class GradeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGradeRepository _gradeRepository;

        public GradeController(IUnitOfWork unitOfWork, IGradeRepository gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _gradeRepository = gradeRepository;
        }

        //
        // GET: /Grade/

        public ActionResult Index()
        {
            return View(_gradeRepository.All());
        }

        //
        // GET: /Grade/Details/5

        public ActionResult Details(int id = 0)
        {
            var grade = _gradeRepository.ById(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        //
        // GET: /Grade/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Grade/Create

        [HttpPost]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _gradeRepository.Add(grade);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(grade);
        }

        //
        // GET: /Grade/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var grade = _gradeRepository.ById(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        //
        // POST: /Grade/Edit/5

        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
               _gradeRepository.Update(grade);
              _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(grade);
        }

        //
        // GET: /Grade/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var grade = _gradeRepository.ById(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        //
        // POST: /Grade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var grade = _gradeRepository.ById(id);
            _gradeRepository.Delete(grade);
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