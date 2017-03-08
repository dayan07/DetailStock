using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DetailStock.DAL.Entities
{
    public class Detail
    {
        public int DetailId { get; set; }
        public string NomenclativeCode { get; set; }
        public string DetailName { get; set; }
        public bool SpecialConside { get; set; }
        public int DetailCount { get; set; }
        public DateTime DateAdded { get; set; }

        // Ссылка на кладовщика
        public Stockman Stockman { get; set; }
        public int StockmanId { get; set; }
        
    }
}
