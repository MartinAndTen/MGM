using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.ViewModels;

namespace GroupProjectRestaurangMVC01.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            RestaurantViewModel viewModel = new RestaurantViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(RestaurantViewModel model)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                if (model.SearchName!=null)
                {
                    List<Restaurant> searchResult =
                         db.Restaurants.Where(c => c.Name.Contains(model.SearchName)).ToList();
                    model.Restaurants = searchResult;
                }

           

                return View(model);
            }

        }
    }
}

