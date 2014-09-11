using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.Repository;
using GroupProjectRestaurangMVC01.ViewModels;

namespace GroupProjectRestaurangMVC01.Controllers
{
    public class RestaurantController : Controller
    {
        //
        // GET: /Restaurant/
        private readonly RestaurantRepository _restaurantRepository = new RestaurantRepository();

        public ActionResult Index(Guid? id)
        {
            RestaurantViewModel viewModel = new RestaurantViewModel();
            if (id.HasValue)
            {
                Restaurant restaurant = _restaurantRepository.GetRestaurantById(id.Value);
                if (restaurant != null)
                {
                    viewModel.Restaurant = restaurant;
                    return View(viewModel);
                }
            }

            List<Restaurant> restaurants = _restaurantRepository.GetAllRestaurantsToList();
            viewModel.Restaurants = restaurants;
            return View(viewModel);
        }

    }
}
