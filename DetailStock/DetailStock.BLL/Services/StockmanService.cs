using DetailStock.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DetailStock.BLL.DTO;
using DetailStock.DAL.Interfaces;
using AutoMapper;
using DetailStock.BLL.Infrastructure;
using DetailStock.DAL.Entities;

namespace StockmanStock.BLL.Services
{
    public class StockmanService : IStockmanService
    {
        IUnitOfWork Database { get; set; }

        public StockmanService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void InsertStockman(StockmanDTO StockmanDTO)
        {
            Stockman Stockman = new Stockman
            {
                StockmanId = StockmanDTO.StockmanId,
                StockmanName = StockmanDTO.StockmanName,
               
            };

            Database.Stockmen.Create(Stockman);
            Database.Save();
        }

        public IEnumerable<StockmanDTO> GetStockmen()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            Mapper.Initialize(cfg => cfg.CreateMap<Stockman, StockmanDTO>());
            return Mapper.Map<IEnumerable<Stockman>, List<StockmanDTO>>(Database.Stockmen.GetAll());
        }

        public StockmanDTO GetStockmanById(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id кладовщика", "");
            var Stockman = Database.Stockmen.Get(id.Value);
            if (Stockman == null)
                throw new ValidationException("Кладовщик не найден", "");
           // применяем автомаппер для проекции Stockman на StockmanDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Stockman, StockmanDTO>());
            return Mapper.Map<Stockman, StockmanDTO>(Stockman);
        }


        public void UpdateStockman(StockmanDTO StockmanDTO)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<StockmanDTO, Stockman>());
            var newStockman= Mapper.Map<StockmanDTO, Stockman>(StockmanDTO);
           // Database.Stockmen.Update(newStockman);
            if (StockmanDTO.StockmanId == 0)
                Database.Stockmen.Create(newStockman);
            else
            {
                Stockman man = Database.Stockmen.Get(newStockman.StockmanId);
                if (man != null)
                {
                    man.StockmanId = newStockman.StockmanId;
                    man.StockmanName = newStockman.StockmanName;
                    Database.Stockmen.Update(man);
                }
            }

        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public StockmanDTO DeleteStockman(int id)
        {
            Stockman stockman = null;
            try
            {
                stockman = Database.Stockmen.Get(id);
                if (stockman.Details.Count == 0)
                {
                    var stock = Database.Stockmen.Delete(id);
                    // применяем автомаппер для проекции stockman на stockmanDTO
                    Mapper.Initialize(cfg => cfg.CreateMap<Stockman, StockmanDTO>());
                    return Mapper.Map<Stockman, StockmanDTO>(stock);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (ValidationException ex)
            {
                ex=new ValidationException("Кладовщик не найден","");
            }
            catch (Exception ex)
            {
                ex=new Exception("Кладовщик не может быть удален, так как на нем числятся детали!!!");
                
            }
            return Mapper.Map<Stockman, StockmanDTO>(stockman);
        }
    }
}
