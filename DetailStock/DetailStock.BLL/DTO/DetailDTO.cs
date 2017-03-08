using DetailStock.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.BLL.DTO
{
    public class DetailDTO
    {
        public int DetailId { get; set; }
        public string NomenclativeCode { get; set; }
        public string DetailName { get; set; }
        public bool SpecialConside { get; set; }
        public int DetailCount { get; set; }
        public DateTime DateAdded { get; set; }
        public int StockmanId { get; set; }
        public string StockmanName { get; set; }



    }
}
