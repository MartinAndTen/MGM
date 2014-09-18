using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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
                 List<Restaurant> searchResult = new List<Restaurant>();
                //if (searchResult.Count==0)
                //{
                //    //Do Error code...
                    
                //}

                 if (model.SearchName!=null && model.SearchCity==null)
                {
                   searchResult =
                   db.Restaurants.Where(c => c.Name.Contains(model.SearchName)).ToList();
                   model.Restaurants = searchResult;
                }
                else if (model.SearchCity != null && model.SearchName==null)
                {
                    searchResult =
                    db.Restaurants.Where(c => c.City.Contains(model.SearchCity)).ToList();
                    model.Restaurants = searchResult;
                }

                else if (model.SearchName != null && model.SearchCity != null)
                {
                    searchResult =
                    db.Restaurants.Where(c => c.Name.Contains(model.SearchName)).ToList();
                    model.Restaurants = searchResult;
                }
                else if (model.SearchName == null && model.SearchCity == null)
                {
                    searchResult =
                        db.Restaurants.ToList();
                    model.Restaurants = searchResult;

                }

            
              
               
                
                return View(model);
            }
        }
    }
}

