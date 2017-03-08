using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DetailStock.DAL.Entities
{
    public class Stockman
    {
        public int StockmanId { get; set; }
        public string StockmanName { get; set; }


        // Ссылка на детали
        public ICollection<Detail> Details { get; set; }
        public Stockman()
        {
            Details = new List<Detail>();
        }

    }
}
