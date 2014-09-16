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
        public Restaurant Restaurant { get; set; }
        [Required]
        [Display(Name="Restaurant Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Zip-Code")]
        public int Zipcode { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required]
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
        [Required]
        [Display(Name = "Restaurant email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Open for booking")]
        public bool Activated { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}