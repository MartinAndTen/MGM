using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly AccountRepository _accountRepository = new AccountRepository();

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
            UserProfile userProfile = _accountRepository.GetUserProfileByUserId(userId);
            Restaurant restaurant = _restaurantRepository.GetRestaurantByUserId(userId);
            if (restaurant != null)
            {
                //viewModel.Restaurant = restaurant;
                return RedirectToAction("Edit", "Restaurant", new {id = restaurant.Id});
            }
            if (userProfile != null)
            {
                viewModel.Name = userProfile.RestaurantName;
                return View(viewModel);
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestaurantViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                string photo;
                //Add Image To Library
                if (file != null)
                {
                    var result = _restaurantRepository.UploadImageToRestaurant(file);
                    photo = result.Photo;
                }
                else
                {
                    photo = _restaurantRepository.DefaultNoImageLocation;
                }

                int userId = WebSecurity.CurrentUserId;
                bool restaurantCreatedSuccessfully = _restaurantRepository.CreateRestaurant(userId, model.Name, model.Description, model.Address, model.Zipcode, model.Phone, model.City, model.TotalSeats, model.Capacity, model.MaxSeatPerBooking, model.Rating, photo, model.Email, model.Activated);

                if (restaurantCreatedSuccessfully)
                {
                    Restaurant theNewRestaurant = _restaurantRepository.GetRestaurantByUserId(userId);
                    return RedirectToAction("Index", "Restaurant", new { id = theNewRestaurant.Id });
                }
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(Guid? id)
        {
           
                RestaurantViewModel viewModel = new RestaurantViewModel();
                if (Request.IsAuthenticated)
                {
                    int userId = WebSecurity.CurrentUserId;
                    Restaurant restaurantToEdit = _restaurantRepository.GetRestaurantByUserId(userId);
                    viewModel = _restaurantRepository.AddRestaurantToViewModel(restaurantToEdit);
                }
                return View(viewModel);
            
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RestaurantViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    int userId = WebSecurity.CurrentUserId;
                    bool updateResult = _restaurantRepository.UpdateRestaurant(userId, model, file);
                    if (updateResult)
                    {
                        Restaurant theNewRestaurant = _restaurantRepository.GetRestaurantByUserId(userId);
                        return RedirectToAction("Index", "Restaurant", new { id = theNewRestaurant.Id });
                    }
                }
            }
            return RedirectToAction("Index","Restaurant");
        }
        
        
        /// <summary>
        /// Add/Edit/Delete Table Methods
        /// </summary>
        /// <returns></returns>

        [Authorize]
        public ActionResult Table()
        {
            TableViewModel viewModel = new TableViewModel();
            if (Request.IsAuthenticated)
            {
                var userId = WebSecurity.CurrentUserId;
                Restaurant currentRestaurant = _restaurantRepository.GetRestaurantByUserId(userId);

                viewModel.Restaurant = currentRestaurant;

            }

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddTable(TableViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool returnValue = _restaurantRepository.CreateTable(WebSecurity.CurrentUserId,model);
            }
            return RedirectToAction("Table","Restaurant");
        }

        [Authorize]
        public ActionResult EditTable(int id)
        {
            TableViewModel viewModel = new TableViewModel();
            viewModel.Table = _restaurantRepository.GetTableById(id);
            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult EditTable(int id, TableViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool returnValue = _restaurantRepository.EditTable(WebSecurity.CurrentUserId,id, model);
            }
            return RedirectToAction("Table", "Restaurant");
        }

        [Authorize]
        public ActionResult DeleteTable(int id)
        {
            var userId = WebSecurity.CurrentUserId;
            bool returnValue = _restaurantRepository.DeleteTable(userId, id);
            return RedirectToAction("Table","Restaurant");
        }

        [Authorize]
        public ActionResult Reservations()
        {
            RestaurantViewModel viewModel = new RestaurantViewModel();
            if (Request.IsAuthenticated)
            {
                Restaurant userRestaurant = _restaurantRepository.GetRestaurantByUserId(WebSecurity.CurrentUserId);
                if (userRestaurant != null)
                {
                    viewModel.Restaurant = userRestaurant;
                }
            }

            return View(viewModel);
        }
    }
}