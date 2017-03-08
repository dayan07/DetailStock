using DetailStock.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailStock.BLL.DTO
{
   public class StockmanDTO
    {
        public int StockmanId { get; set; }
        public string StockmanName { get; set; }
       
    }
}
