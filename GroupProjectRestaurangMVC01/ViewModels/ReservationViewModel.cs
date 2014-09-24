using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GroupProjectRestaurangMVC01.ViewModels
{
    public class ReservationViewModel
    {
        public Restaurant Restaurant { get; set; }
        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Table> Tables { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yy/mm/dd}")]
        [Required(ErrorMessage = "A date is required")]
        public DateTime Date { get; set; }
        public string TimeString { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public string Day { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "A name is required")]
        public string CustomerName { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "A phone number is required")]
        public string CustomerPhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string ContactEmail { get; set; }

        [Display(Name = "Persons in party")]
        [Required(ErrorMessage = "Number of persons is required")]
        public int TotalGuests { get; set; }

        public string Result { get; set; }

        public int ammountOfButtonsToGenerate { get; set; }

        //public List<DateTime> proppmedtider { get; set; }

        public DateTime openTime { get; set; }
        
        public DateTime closeTime { get; set; }

        public List<string> ButtonList { get; set; }
      
    }
}