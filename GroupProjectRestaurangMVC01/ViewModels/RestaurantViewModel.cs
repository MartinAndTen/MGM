using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;

namespace GroupProjectRestaurangMVC01.ViewModels
{
    public class RestaurantViewModel
    {

        /// <summary>
        /// Restaurant Props
        /// </summary>

        public Restaurant Restaurant { get; set; }

        [Required(ErrorMessage = "Restaurant name is required")]
        [Display(Name="Restaurant Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Zip-code is required")]
        [Display(Name = "Zip-Code")]
        [RegularExpression(@"^\d{5}-\d{4}|\d{5}|[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessage ="Entered number is not a valid Zip-code")]
        public int Zipcode { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Display(Name = "Phone")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Total seats")]
        public Nullable<int> TotalSeats { get; set; }

        [Display(Name = "Capacity")]
        public Nullable<int> Capacity { get; set; }

        [Display(Name = "Maximun Ordersize")]
        public Nullable<int> MaxSeatPerBooking { get; set; }

        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Restaurant email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Entered email is not a valid email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Open for booking")]
        public bool Activated { get; set; }

        public List<Restaurant> Restaurants { get; set; }


        /// <summary>
        /// Restaurant Search props
        /// </summary>

        [Display(Name="Name")]
        public string SearchName { get; set; }

        [Display(Name="City")]
        public string SearchCity { get; set; }

        public string Result { get; set; }


        
        public Days Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public enum Days
        {
            Monday,
            Tuesday,
            Wendsday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        
    }
}