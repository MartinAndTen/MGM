using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectRestaurangMVC01.ViewModels
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yy/mm/dd}")]
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public string CustomerName { get; set; }
        public int CustomerPhone { get; set; }
    }
}