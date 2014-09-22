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
             RestaurantViewModel result = _searchRepository.SearchResult(model);

                return View(result);
            }
        }
    }


