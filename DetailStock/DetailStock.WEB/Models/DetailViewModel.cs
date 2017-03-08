using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetailStock.WEB.Models
{
    public class DetailViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int DetailId { get; set; }
        [Display(Name = "Номенклатурный код")]
        [Required(ErrorMessage = "Введите код")]
        [RegularExpression(@"^[a-zA-Z]{3}-\d{6}$", ErrorMessage = "Некорректный код")]
        public string NomenclativeCode { get; set; }
        [Display(Name = "Название детали")]
        [Required(ErrorMessage = "Введите деталь")]
        public string DetailName { get; set; }
        [Display(Name = "Особоучитываемая")]
        public bool SpecialConside { get; set; }
        [Display(Name = "Количество деталей")]
        [Required(ErrorMessage = "Введите количество деталей")]
        [Range(1, 2147483647, ErrorMessage = "Недопустимое количество")]
        public int DetailCount { get; set; }
        [Display(Name = "Дата добавления")]
        [Required(ErrorMessage = "Введите дату")]
        public DateTime DateAdded { get; set; }

        // Ссылка на кладовщика
        public int StockmanId { get; set; }
        [Display(Name = "Кладовщик")]
        public string StockmanName { get; set; }
    }
}