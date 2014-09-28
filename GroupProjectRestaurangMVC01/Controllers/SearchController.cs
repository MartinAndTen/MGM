using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.Repository;
using GroupProjectRestaurangMVC01.ViewModels;

namespace GroupProjectRestaurangMVC01.Controllers
{
    public class SearchController : Controller
    {
        private readonly SearchRepository _searchRepository = new SearchRepository();
        private readonly RestaurantRepository _restaurantRepository = new RestaurantRepository();
        //
        // GET: /Search/

        public ActionResult Index()
        {

            RestaurantViewModel viewModel = new RestaurantViewModel();
            viewModel.Restaurants = _restaurantRepository.GetAllRestaurantsToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(RestaurantViewModel model)
        {
             RestaurantViewModel result = _searchRepository.SearchResult(model);

                return View(result);
            }
        }
    }


