using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetailStock.WEB.Models
{
    public class StockmanViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int StockmanId { get; set; }
        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО кладовщика")]
        public string StockmanName { get; set; }
        
    }
}