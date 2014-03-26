using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keramatian.Infrastructure;
using Keramatian.Infrastructure.Logging;
using Keramatian.Models;
using Keramatian.Repository;
using Keramatian.Services;
using Keramatian.ViewModel;
using PagedList;


namespace Keramatian.Controllers
{
    public class LeatherCarpetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeatherCarpetRepository _leatherCarpetRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IBackgroundColorRepository _backgroundColorRepository;
        private readonly IPlainRepository _plainRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly ILeatherCarpetService _leatherCarpetService;
        private readonly IDesignRepository _designRepository;
        public LeatherCarpetController(IUnitOfWork unitOfWork, ILeatherCarpetRepository leatherCarpetRepository, IGradeRepository gradeRepository, IBackgroundColorRepository backgroundColorRepository, IPlainRepository plainRepository, ISizeRepository sizeRepository, ILeatherCarpetService leatherCarpetService, IDesignRepository designRepository)
        {
            _unitOfWork = unitOfWork;
            _leatherCarpetRepository = leatherCarpetRepository;
            _gradeRepository = gradeRepository;
            _backgroundColorRepository = backgroundColorRepository;
            _plainRepository = plainRepository;
            _sizeRepository = sizeRepository;
            _leatherCarpetService = leatherCarpetService;
            _designRepository = designRepository;
        }

        //
        // GET: /Carpet/

        public ActionResult Index()
        {
            return View(_leatherCarpetRepository.All());
        }

        //
        // GET: /Carpet/Details/5

        public ActionResult Details(int id = 0)
        {
            var carpet = _leatherCarpetRepository.ById(id);
            if (carpet == null)
            {
                return HttpNotFound();
            }
            return View(carpet);
        }

        //
        // GET: /Carpet/Create

        public ActionResult Create()
        {
            ViewBag.Grade = new SelectList(_gradeRepository.All(), "Id", "Name");
            ViewBag.BackgroundColor = new SelectList(_backgroundColorRepository.All(), "Id", "Name");
            ViewBag.Plain = new SelectList(_plainRepository.All(), "Id", "Name");
            ViewBag.Design = new SelectList(_designRepository.All(), "Id", "Name");
            return View(new LeatherCarpetDto { Sizes = _leatherCarpetService.PopulateSizeData() });
        }

        //
        // POST: /Carpet/Create

        [HttpPost]
        public ActionResult Create(LeatherCarpetDto leatherCarpetDto, FormCollection formCollection, HttpPostedFileBase imageFiles, HttpPostedFileBase highFiles)
        {
            leatherCarpetDto.LeatherCarpet.Grade = _gradeRepository.ById(int.Parse(formCollection["Grade"]));
            leatherCarpetDto.LeatherCarpet.BackgroundColor = _backgroundColorRepository.ById(int.Parse(formCollection["BackgroundColor"]));
            leatherCarpetDto.LeatherCarpet.Plain = _plainRepository.ById(int.Parse(formCollection["Plain"]));
            leatherCarpetDto.LeatherCarpet.Design = _designRepository.ById(int.Parse(formCollection["Design"]));

            try
            {

                if (imageFiles.ContentLength > 0)
                {

                    var imageName = leatherCarpetDto.LeatherCarpet.Design + DateTime.Now.ToString("ddyyyMMmmhh") + Path.GetExtension(imageFiles.FileName);
                    imageFiles.SaveAs(HttpContext.Server.MapPath("~/Images/leatherCarpet/" + imageName));
                    leatherCarpetDto.LeatherCarpet.ImagePath = "/Images/leatherCarpet/" + imageName;

                }

                if (highFiles.ContentLength > 0)
                {

                    var imageName = "H-" + leatherCarpetDto.LeatherCarpet.Design + DateTime.Now.ToString("ddyyyMMmmhh") + Path.GetExtension(highFiles.FileName);
                    highFiles.SaveAs(HttpContext.Server.MapPath("~/Images/leatherCarpet/" + imageName));
                    leatherCarpetDto.LeatherCarpet.HighQualityImagePath = "/Images/leatherCarpet/" + imageName;
                }
                _leatherCarpetService.SaveCarpet(leatherCarpetDto);

                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ViewBag.Grade = new SelectList(_gradeRepository.All(), "Id", "Name");
                ViewBag.BackgroundColor = new SelectList(_backgroundColorRepository.All(), "Id", "Name");
                ViewBag.Plain = new SelectList(_plainRepository.All(), "Id", "Name");
                ViewBag.Size = new SelectList(_sizeRepository.All(), "Id", "Name");
                ViewBag.Design = new SelectList(_designRepository.All(), "Id", "Name");
                return View(new LeatherCarpetDto() { Sizes = _leatherCarpetService.PopulateSizeData() });

            }




        }

        //
        // GET: /Carpet/Edit/5

        public ActionResult Edit(int id = 0)
        {

            var leatherCarpet = _leatherCarpetRepository.ById(id);
            if (leatherCarpet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Grades = new SelectList(_gradeRepository.All(), "Id", "Name", leatherCarpet.Grade.Id);
            ViewBag.BackgroundColors = new SelectList(_backgroundColorRepository.All(), "Id", "Name", leatherCarpet.BackgroundColor.Id);
            ViewBag.Plains = new SelectList(_plainRepository.All(), "Id", "Name", leatherCarpet.Plain.Id);
            // ViewBag.Sizes = new SelectList(_sizeRepository.All(), "Id", "Name", leatherCarpet.Size.Id);
            ViewBag.Design = new SelectList(_designRepository.All(), "Id", "Name",leatherCarpet.Design.Id);
         
            return View(_leatherCarpetService.GenerateCarpetDtoForEditForm(id));
        }

        //
        // POST: /Carpet/Edit/5

        [HttpPost]
        public ActionResult Edit(LeatherCarpetDto leatherCarpetDtot)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _leatherCarpetService.UpdateCarpet(leatherCarpetDtot);
                    _unitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    var carpet = leatherCarpetDtot.LeatherCarpet;
                    ViewBag.Grades = new SelectList(_gradeRepository.All(), "Id", "Name", carpet.Grade.Id);
                    ViewBag.BackgroundColors = new SelectList(_backgroundColorRepository.All(), "Id", "Name", carpet.BackgroundColor.Id);
                    ViewBag.Plains = new SelectList(_plainRepository.All(), "Id", "Name", carpet.Plain.Id);
                    ViewBag.Design = new SelectList(_designRepository.All(), "Id", "Name", carpet.Design.Id);
                    return View(_leatherCarpetService.GenerateCarpetDtoForEditForm(carpet.Id));
                }
            }
            return View(_leatherCarpetService.GenerateCarpetDtoForEditForm(leatherCarpetDtot.LeatherCarpet.Id));
        }

        //
        // GET: /Carpet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var carpet = _leatherCarpetRepository.ById(id);
            if (carpet == null)
            {
                return HttpNotFound();
            }
            return View(carpet);
        }

        //
        // POST: /Carpet/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var leatherCarpet = _leatherCarpetRepository.ById(id);
                System.IO.File.Delete(HttpContext.Server.MapPath(leatherCarpet.ImagePath));
                System.IO.File.Delete(HttpContext.Server.MapPath(leatherCarpet.HighQualityImagePath));
                _leatherCarpetRepository.Delete(leatherCarpet);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                var leatherCarpet = _leatherCarpetRepository.ById(id);
                _leatherCarpetRepository.Delete(leatherCarpet);
                _unitOfWork.Commit();
            }

            return RedirectToAction("Index");
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