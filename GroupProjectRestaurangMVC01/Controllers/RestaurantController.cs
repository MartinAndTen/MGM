using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.Repository;
using GroupProjectRestaurangMVC01.ViewModels;
using WebMatrix.WebData;

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

        [Authorize]
        public ActionResult Create()
        {
            RestaurantViewModel viewModel = new RestaurantViewModel();
            var userId = WebSecurity.CurrentUserId;
            UserProfile userProfile = _restaurantRepository.GetUserProfileByUserId(userId);
            Restaurant restaurant = _restaurantRepository.GetRestaurantByUserId(userId);
            if (restaurant != null)
            {
                viewModel.Restaurant = restaurant;
                return View(viewModel);
            }
            if (userProfile != null)
            {
                Restaurant newRestaurant = new Restaurant();
                newRestaurant.Name = userProfile.RestaurantName;
                viewModel.Restaurant = newRestaurant;
                return View(viewModel);
            }
            
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(RestaurantViewModel model)
        {


            return View(model);
            //return RedirectToAction("Index", new {id = model.Restaurant.Id});
        }

    }
}
