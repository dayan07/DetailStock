using AutoMapper;
using DetailStock.BLL.DTO;
using DetailStock.BLL.Infrastructure;
using DetailStock.BLL.Interfaces;
using DetailStock.DAL.Entities;
using DetailStock.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DetailStock.BLL.Services
{
    public class DetailService : IDetailService
    {
        IUnitOfWork Database { get; set; }

        public DetailService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void InsertDetail(DetailDTO detailDTO)
        {
            Stockman stockman = Database.Stockmen.Get(detailDTO.StockmanId);
            // валидация
            if (stockman == null)
                throw new ValidationException("Кладовщик не найден", "");
            if (detailDTO.NomenclativeCode!=null)
            {
                String input = @"^[a-zA-Z]{3}-\d{6}$";
                if (Regex.IsMatch(detailDTO.NomenclativeCode, input))
                {
                    Detail detail = new Detail
                    {
                        DetailId = detailDTO.DetailId,
                        NomenclativeCode = detailDTO.NomenclativeCode,
                        DetailName = detailDTO.DetailName,
                        SpecialConside = detailDTO.SpecialConside,
                        DetailCount = detailDTO.DetailCount,
                        DateAdded = detailDTO.DateAdded,
                        StockmanId = stockman.StockmanId,
                        Stockman = stockman
                    };
                    Database.Details.Create(detail);
                    Database.Save();
                }
                else
                {
                    throw new ValidationException("Номенклатурный код не соответствует шаблону", "");
                }
            }
            else
            {
                throw new ValidationException("Заполните номенклатурный код", "");
            }
        }

        public IEnumerable<DetailDTO> GetDetails()
        {
            var results = Database.Details.GetAll();
            // применяем автомаппер для проекции одной коллекции на другую
            Mapper.Initialize(cfg => cfg.CreateMap<Detail, DetailDTO>().ForMember("StockmanName", opt => opt.MapFrom(c => c.Stockman.StockmanName)));
            return Mapper.Map<IEnumerable<Detail>, List<DetailDTO>>(results);
        }

        public DetailDTO GetDetailById(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id детали", "");
            var detail = Database.Details.Get(id.Value);
            if (detail == null)
                throw new ValidationException("Деталь не найдена", "");
            // применяем автомаппер для проекции Detail на DetailDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Detail,DetailDTO>());
            return Mapper.Map<Detail, DetailDTO>(detail);
        }

      
        public DetailDTO GetDetail(string Code)
        {
            if (Code == null)
                throw new ValidationException("Заполните код искомой детали", "");
            var array = Database.Details.GetAll();
            var detail = from det in array
                         where det.NomenclativeCode == Code
                         select det;
            if (detail == null)
                throw new ValidationException("Деталь не найдена", "");
            // применяем автомаппер для проекции Detail на DetailDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Detail, DetailDTO>());
            return Mapper.Map<Detail, DetailDTO>(detail.First());
        }

        public void UpdateDetail(DetailDTO detailDTO)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DetailDTO,Detail>());
            Detail newdetail=Mapper.Map<DetailDTO,Detail>(detailDTO);
            if (detailDTO.DetailId == 0)
                Database.Details.Create(newdetail);
            else
            {
                Detail detail = Database.Details.Get(newdetail.DetailId);
                if (detail != null)
                {
                    detail.DetailId = newdetail.DetailId;
                    detail.NomenclativeCode = newdetail.NomenclativeCode;
                    detail.DetailName = newdetail.DetailName;
                    detail.SpecialConside = newdetail.SpecialConside;
                    detail.DetailCount = newdetail.DetailCount;
                    detail.DateAdded = newdetail.DateAdded;
                    detail.StockmanId = newdetail.StockmanId;
                    detail.Stockman = newdetail.Stockman;
                    Database.Details.Update(detail);
                }
            }
       }
      
        public DetailDTO DeleteDetail(int detailID)
        {
            var detail = Database.Details.Get(detailID);
            if (detail == null)
                throw new ValidationException("Деталь не найдена", "");
            var det=Database.Details.Delete(detailID);
            // применяем автомаппер для проекции Detail на DetailDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Detail, DetailDTO>());
            return Mapper.Map<Detail, DetailDTO>(det);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}