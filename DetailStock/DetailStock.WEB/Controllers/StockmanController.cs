using AutoMapper;
using DetailStock.BLL.DTO;
using DetailStock.BLL.Infrastructure;
using DetailStock.BLL.Interfaces;
using DetailStock.WEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetailStock.WEB.Controllers
{
    public class StockmanController : Controller
    {
        IStockmanService stockmanService;
        public StockmanController(IDetailService serv, IStockmanService stServ)
        {
           stockmanService = stServ;
        }
        // GET: Stockman
        public ActionResult IndexStockmen()
        {
            IEnumerable<StockmanDTO> stockmanDTOs = stockmanService.GetStockmen();
            Mapper.Initialize(cfg => cfg.CreateMap<StockmanDTO, StockmanViewModel>());
            var stockmen = (Mapper.Map<IEnumerable<StockmanDTO>, List<StockmanViewModel>>(stockmanDTOs));

            return View(stockmen);
        }

        // GET: Stockman/Create
        public ActionResult Create()
        {
            try
            {
                return PartialView("Create");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: Stockman/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockmanViewModel stockman)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<StockmanViewModel, StockmanDTO>());
                    var stockmanDTO = Mapper.Map<StockmanViewModel, StockmanDTO>(stockman);
                    stockmanService.InsertStockman(stockmanDTO);
                    return PartialView("Success");
                }
             }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return PartialView(stockman);
        }

        // GET: Stockman/Edit/5
        public ActionResult Edit(int id)
        {
            StockmanDTO man = stockmanService.GetStockmanById(id);

            Mapper.Initialize(cfg => cfg.CreateMap<StockmanDTO, StockmanViewModel >());
            var stockmanview = Mapper.Map<StockmanDTO, StockmanViewModel>(man);
            if (stockmanview != null)
            {
                return PartialView(stockmanview);
            }
           return View("IndexStockmen");
        }
              
        // POST: Stockman/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StockmanViewModel view)
        {
            if (ModelState.IsValid)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<StockmanViewModel, StockmanDTO>());
                    var dto = Mapper.Map<StockmanViewModel, StockmanDTO>(view);
                    stockmanService.UpdateStockman(dto);
                    return PartialView("Success");
                }
            return PartialView(view);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            StockmanDTO deletedDTO = stockmanService.GetStockmanById(id);
            if (deletedDTO == null)
            {
                return HttpNotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<StockmanDTO, StockmanViewModel>());
            var view = Mapper.Map<StockmanDTO, StockmanViewModel>(deletedDTO);
            return PartialView("Delete", view);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StockmanDTO deletedDTO = stockmanService.GetStockmanById(id);
            if (deletedDTO == null)
            {
                return HttpNotFound();
            }
            else if (deletedDTO != null)
            {
                StockmanDTO deleted = stockmanService.DeleteStockman(id);
            }
            return RedirectToAction("IndexStockmen");
        }
              
    }
}
