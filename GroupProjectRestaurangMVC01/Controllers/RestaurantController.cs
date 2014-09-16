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

                string photo = String.Empty;
                //Add Image To Library
                if (file != null)
                {
                    var result = _restaurantRepository.UploadImageToRestaurant(file);
                    photo = result.Photo;
                }
                else
                {
                    photo = "~/Assets/Images/800px-No_Image_Wide.png";
                }

                int userId = WebSecurity.CurrentUserId;
                bool restaurantCreatedSuccessfully = _restaurantRepository.CreateRestaurant(userId, model.Name, model.Description, model.Address, model.Zipcode, model.Phone, model.City, model.TotalSeats, model.Capacity, model.MaxSeatPerBooking, model.Rating, photo, model.Email, model.Activated);

                if (restaurantCreatedSuccessfully)
                {
                    Restaurant theNewRestaurant = _restaurantRepository.GetRestaurantByUserId(userId);
                    return RedirectToAction("Index", "Restaurant", new { id = theNewRestaurant.Id });
                }
            }
            return View(model);
        }


        public ActionResult Edit(Guid id)
        {
            RestaurantViewModel viewModel = new RestaurantViewModel();
            if (Request.IsAuthenticated)
            {
                int userId = WebSecurity.CurrentUserId;
                Restaurant restaurantToEdit = _restaurantRepository.GetRestaurantByUserId(userId);
                viewModel.Restaurant = restaurantToEdit;
                viewModel.Name = restaurantToEdit.Name;
                viewModel.Description = restaurantToEdit.Description;
                viewModel.Address = restaurantToEdit.Address;
                viewModel.Zipcode = restaurantToEdit.Zipcode;
                viewModel.Phone = restaurantToEdit.Phone;
                viewModel.City = restaurantToEdit.City;
                viewModel.TotalSeats = restaurantToEdit.TotalSeats;
                viewModel.Capacity = restaurantToEdit.Capacity;
                viewModel.MaxSeatPerBooking = restaurantToEdit.MaxSeatPerBooking;
                viewModel.Email = restaurantToEdit.Email;
                viewModel.Activated = restaurantToEdit.Activated;
            }
            return View(viewModel);
        }
    }
}
