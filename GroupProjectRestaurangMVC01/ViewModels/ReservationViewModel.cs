using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;

namespace GroupProjectRestaurangMVC01.ViewModels
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}