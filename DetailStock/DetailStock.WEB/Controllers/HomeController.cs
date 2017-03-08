using AutoMapper;
using DetailStock.BLL.DTO;
using DetailStock.BLL.Infrastructure;
using DetailStock.BLL.Interfaces;
using DetailStock.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DetailStock.WEB.Controllers
{
    public class HomeController : Controller
    {
        IDetailService detailService;
        IStockmanService stockmenService;
        public HomeController(IDetailService serv, IStockmanService stServ)
        {
            detailService = serv;
            stockmenService = stServ;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string filter)
        {
            ViewBag.filter = filter;
            return View();
        }
        public ActionResult Details(string filter=null)
        {
            IEnumerable<DetailDTO> detailDTOs = detailService.GetDetails();
            return View("DetailSearch", filter == null ? detailDTOs : detailService.GetDetails().Where(a => a.NomenclativeCode.Contains(filter)).ToList());
        }
        public ActionResult Edit(int id)
        {
            DetailDTO det = detailService.GetDetailById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<DetailDTO, DetailViewModel>());
            var detailview = Mapper.Map<DetailDTO, DetailViewModel>(det);
            if (detailview != null)
            {
                    GetSelectListStockmen();
                return PartialView(detailview);
            }
               return View("Index");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DetailViewModel view)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DetailViewModel, DetailDTO>());
                var dto = Mapper.Map<DetailViewModel, DetailDTO>(view);
                detailService.UpdateDetail(dto);
                return PartialView("SuccessDetail");
            }
            GetSelectListStockmen();
            return PartialView(view);
        }
       
        public ActionResult InsertDetail()
        {
            try
            {
                GetSelectListStockmen();
                return PartialView("InsertDetail");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertDetail(DetailViewModel detail)
        {
            try
            {
                if (detail != null)
                {
                    if (ModelState.IsValid)
                    {
                        Mapper.Initialize(cfg => cfg.CreateMap<DetailViewModel, DetailDTO>());
                        var detailDTO = Mapper.Map<DetailViewModel, DetailDTO>(detail);
                        detailService.InsertDetail(detailDTO);
                        return PartialView("SuccessDetail");
                    }
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            GetSelectListStockmen();
            return PartialView(detail);
        }

        private void GetSelectListStockmen()
        {
            IEnumerable<StockmanDTO> menDTOs = stockmenService.GetStockmen();
            Mapper.Initialize(cfg => cfg.CreateMap<StockmanDTO, StockmanViewModel>());
            IEnumerable<StockmanViewModel> stockmen = (Mapper.Map<IEnumerable<StockmanDTO>, List<StockmanViewModel>>(menDTOs));

            // Формируем список кладовщиков для передачи в представление
            SelectList men = new SelectList(stockmen, "StockmanId", "StockmanName");
            ViewBag.Teams = men;
        }

        //[HttpGet]
        public ActionResult DeleteDetail(int id)
        {
            DetailDTO deletedDTO = detailService.GetDetailById(id);
            if (deletedDTO != null)
            {
                return PartialView("DeleteDetail", deletedDTO);
            }
            return View("Index");
            
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("DeleteDetail")]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailDTO deletedDTO = detailService.GetDetailById(id);
            if (deletedDTO == null)
            {
                return HttpNotFound();
            }
            if (deletedDTO != null)
            {
                DetailDTO deleted = detailService.DeleteDetail(id);
               
            }
            return RedirectToAction("Index");
        }
      
       protected override void Dispose(bool disposing)
        {
            detailService.Dispose();
            base.Dispose(disposing);
        }
    }
}