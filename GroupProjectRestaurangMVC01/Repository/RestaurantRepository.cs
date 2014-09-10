using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;

namespace GroupProjectRestaurangMVC01.Repository
{
    public class RestaurantRepository
    {
        public Restaurant GetRestaurantById(Guid id)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Restaurant result = db.Restaurants.Include("ClosedForBookings")
                        .Include("OpenForBookings")
                        .Include("Reservations")
                        .Include("Tables")
                        .FirstOrDefault(c => c.Id.Equals(id));
                return result;
            }
        }
    }
}