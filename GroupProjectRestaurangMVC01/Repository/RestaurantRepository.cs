using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupProjectRestaurangMVC01.Models;

namespace GroupProjectRestaurangMVC01.Repository
{
    public class RestaurantRepository
    {
        public bool AddOrUpdateRestaurant(Restaurant restaurant)
        {
            bool returnValue = false;
            try
            {
                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    db.Restaurants.AddOrUpdate(restaurant);
                    db.SaveChanges();
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public bool DeleteRestaurant(Guid restaurantId)
        {
            bool returnValue = false;
            try
            {
                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    Restaurant restaurantToDelete = db.Restaurants.FirstOrDefault(c => c.Id.Equals(restaurantId));
                    if (restaurantToDelete != null)
                    {
                        db.Restaurants.Remove(restaurantToDelete);
                        db.SaveChanges();
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

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

        public Restaurant GetRestaurantByUserId(int id)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Restaurant result = db.Restaurants.Include("ClosedForBookings")
                        .Include("OpenForBookings")
                        .Include("Reservations")
                        .Include("Tables")
                        .FirstOrDefault(c => c.UserId.Equals(id));
                return result;
            }
        }

        public List<Restaurant> GetAllRestaurantsToList()
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                List<Restaurant> result = db.Restaurants.Include("ClosedForBookings")
                    .Include("OpenForBookings")
                    .Include("Reservations")
                    .Include("Tables").ToList();
                return result;
            }
        }

        
    }
}