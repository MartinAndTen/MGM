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
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Zipcode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string City { get; set; }
        public Nullable<int> TotalSeats { get; set; }
        public Nullable<int> Capacity { get; set; }
        public Nullable<int> MaxSeatPerBooking { get; set; }
        public string Rating { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Activated { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}