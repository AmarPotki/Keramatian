using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Keramatian.Infrastructure;
using Keramatian.Models;
using Keramatian.Repository;
using Keramatian.Services;
using Keramatian.ViewModel;
using PagedList;

namespace Keramatian.Controllers
{
    public class CarpetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICarpetRepository _carpetRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IBackgroundColorRepository _backgroundColorRepository;
        private readonly IPlainRepository _plainRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly ICarpetService _carpetService;
        public CarpetController(IUnitOfWork unitOfWork, ICarpetRepository carpetRepository, IGradeRepository gradeRepository, IBackgroundColorRepository backgroundColorRepository, IPlainRepository plainRepository, ISizeRepository sizeRepository, ICarpetService carpetService)
        {
            _unitOfWork = unitOfWork;
            _carpetRepository = carpetRepository;
            _gradeRepository = gradeRepository;
            _backgroundColorRepository = backgroundColorRepository;
            _plainRepository = plainRepository;
            _sizeRepository = sizeRepository;
            _carpetService = carpetService;
        }

        //
        // GET: /Carpet/

        public ActionResult Index()
        {

            return View(_carpetRepository.All());
        }

        //
        // GET: /Carpet/Details/5

        public ActionResult Details(int id = 0)
        {
            var carpet = _carpetRepository.ById(id);
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

            return View(new CarpetDto { Sizes = _carpetService.PopulateSizeData() });
        }

        //
        // POST: /Carpet/Create

        [HttpPost]
        public ActionResult Create(FormCollection formCollection, HttpPostedFileBase imageFiles, HttpPostedFileBase highFiles, CarpetDto carpetDto)
        {

            carpetDto.Carpet.Grade = _gradeRepository.ById(int.Parse(formCollection["Grade"]));
            carpetDto.Carpet.BackgroundColor = _backgroundColorRepository.ById(int.Parse(formCollection["BackgroundColor"]));
            carpetDto.Carpet.Plain = _plainRepository.ById(int.Parse(formCollection["Plain"]));


            try
            {

                if (imageFiles.ContentLength > 0)
                {

                    var imageName = carpetDto.Carpet.Design + DateTime.Now.ToString("ddyyyMMmmhh") + System.IO.Path.GetExtension(imageFiles.FileName);
                    imageFiles.SaveAs(HttpContext.Server.MapPath("~/Images/Carpet/" + imageName));
                    carpetDto.Carpet.ImagePath = "/Images/Carpet/" + imageName;
                    carpetDto.Carpet.ImageName = imageName;

                }

                if (highFiles.ContentLength > 0)
                {
                    var imageName = "H-" + carpetDto.Carpet.Design + DateTime.Now.ToString("ddyyyMMmmhh") + System.IO.Path.GetExtension(highFiles.FileName);
                    highFiles.SaveAs(HttpContext.Server.MapPath("~/Images/Carpet/" + imageName));
                    carpetDto.Carpet.HighQualityImagePath = "/Images/Carpet/" + imageName;

                }

                _carpetService.SaveCarpet(carpetDto);

                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ViewBag.Grade = new SelectList(_gradeRepository.All(), "Id", "Name");
                ViewBag.BackgroundColor = new SelectList(_backgroundColorRepository.All(), "Id", "Name");
                ViewBag.Plain = new SelectList(_plainRepository.All(), "Id", "Name");
                ViewBag.Size = new SelectList(_sizeRepository.All(), "Id", "Name");

                return View(new CarpetDto { Sizes = _carpetService.PopulateSizeData() });

            }




        }

        //
        // GET: /Carpet/Edit/5

        public ActionResult Edit(int id = 0)
        {

            var carpet = _carpetRepository.GetCarpetWithSize(id);
            if (carpet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Grades = new SelectList(_gradeRepository.All(), "Id", "Name", carpet.Grade.Id);
            ViewBag.BackgroundColors = new SelectList(_backgroundColorRepository.All(), "Id", "Name", carpet.BackgroundColor.Id);
            ViewBag.Plains = new SelectList(_plainRepository.All(), "Id", "Name", carpet.Plain.Id);


            return View(_carpetService.GenerateCarpetDtoForEditForm(id));
        }

        //
        // POST: /Carpet/Edit/5

        [HttpPost]
        public ActionResult Edit(CarpetDto carpetDto, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _carpetService.UpdateCarpet(carpetDto);
                    _unitOfWork.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    var carpet = carpetDto.Carpet;
                    ViewBag.Grades = new SelectList(_gradeRepository.All(), "Id", "Name", carpet.Grade.Id);
                    ViewBag.BackgroundColors = new SelectList(_backgroundColorRepository.All(), "Id", "Name", carpet.BackgroundColor.Id);
                    ViewBag.Plains = new SelectList(_plainRepository.All(), "Id", "Name", carpet.Plain.Id);
                    return View(_carpetService.GenerateCarpetDtoForEditForm(carpet.Id));
                }
            }
            return View(_carpetService.GenerateCarpetDtoForEditForm(carpetDto.Carpet.Id));
        }

        //
        // GET: /Carpet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var carpet = _carpetRepository.ById(id);
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
            var carpet = _carpetRepository.ById(id);
            try
            {
                System.IO.File.Delete(HttpContext.Server.MapPath(carpet.ImagePath));
                System.IO.File.Delete(HttpContext.Server.MapPath(carpet.HighQualityImagePath));
                _carpetRepository.Delete(carpet);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _carpetRepository.Delete(carpet);
                _unitOfWork.Commit();
            }


            return RedirectToAction("Index");
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