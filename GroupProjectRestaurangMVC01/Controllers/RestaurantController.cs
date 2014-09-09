using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.ViewModels;

namespace GroupProjectRestaurangMVC01.Controllers
{
    public class RestaurantController : Controller
    {
        //
        // GET: /Restaurant/

        public ActionResult Index(Guid id)
        {
            RestaurangViewModel viewModel = new RestaurangViewModel();

            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Restaurant restaurant =
                    db.Restaurants.Include("ClosedForBookings")
                        .Include("OpenForBookings")
                        .Include("Reservations")
                        .Include("Tables")
                        .FirstOrDefault(c => c.Id.Equals(id));
            
             
            
            viewModel.Restaurant = restaurant;
            }
            return View(viewModel);
        }

    }
}
