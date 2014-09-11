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
        private RestaurantRepository _restaurantRepository;

        public ActionResult Index(Guid? id)
        {
            RestaurantViewModel viewModel = new RestaurantViewModel();
            if (id.HasValue)
            {
                viewModel.Restaurant = _restaurantRepository.GetRestaurantById(id.Value);
                return View(viewModel);
            }

            viewModel.Restaurants = _restaurantRepository.GetAllRestaurantsToList();
            return View(viewModel);
        }

    }
}
