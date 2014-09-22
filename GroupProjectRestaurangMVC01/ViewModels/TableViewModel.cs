using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;

namespace GroupProjectRestaurangMVC01.ViewModels
{
    public class TableViewModel
    {
        public Restaurant Restaurant { get; set; }
        public Table Table { get; set; }

        [Required(ErrorMessage = "Table name or number is required")]
        [Display(Name ="Name/Number")]
        public string TableName { get; set; }

        [Required(ErrorMessage = "Seats is required")]
        [Display(Name="Seats")]
        public int Seats { get; set; }

    }
}