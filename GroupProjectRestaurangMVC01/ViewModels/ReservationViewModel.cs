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
        public Restaurant Restaurant { get; set; }
        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Table> Tables { get; set; }

        //[Key]
        //public int id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yy/mm/dd}")]
        [Required(ErrorMessage = "A date is required")]
        public DateTime Date { get; set; }

        public string Day { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "A name is required")]
        public string CustomerName { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "A phone number is required")]
        public string CustomerPhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "An email is required")]
        public string ContactEmail { get; set; }

        [Display(Name = "Persons in party")]
        [Required(ErrorMessage = "Number of persons is required")]
        public int TotalGuests { get; set; }
        public bool ConfirmedReservation { get; set; }
    }
}