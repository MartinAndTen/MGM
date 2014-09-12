using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;

namespace GroupProjectRestaurangMVC01.ViewModels
{
    public class RestaurantViewModel
    {
        public Restaurant Restaurant { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public Nullable<int> TotalSeats { get; set; }
        public Nullable<int> Capacity { get; set; }
        public Nullable<int> MaxSeatPerBooking { get; set; }
        public string Rating { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public bool Activated { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}