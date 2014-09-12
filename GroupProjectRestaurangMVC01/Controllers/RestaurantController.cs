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
        public ActionResult Create([Bind(Include="Name,Description,Address,Zipcode,Phone,City,TotalSeats,Capacity,MaxSeatPerBooking,Rating,Photo,Email,Activated")]RestaurantViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                string photo = String.Empty;
                //Add Image To Library
                if (file != null)
                {
                    string fileExtention = Path.GetExtension(file.FileName);
                    //Creating filename to avoid file name conflicts
                    string fileName = Guid.NewGuid().ToString();
                    string pic = fileName + fileExtention;
                    string path = Path.Combine(Server.MapPath("~Assets/UserAssets/Images"), pic);

                    //Nu är filen uppladdad och ska sparas ner
                    file.SaveAs(path);
                    photo = Path.GetFileName(path);
                }

                int userId = WebSecurity.CurrentUserId;
                bool restaurantCreatedSuccessfully = _restaurantRepository.CreateRestaurant(userId, model.Name,model.Description,model.Address,model.Zipcode,model.Phone,model.City,model.TotalSeats,model.Capacity,model.MaxSeatPerBooking,model.Rating,photo,model.Email,model.Activated);

                if (restaurantCreatedSuccessfully)
                {
                    Restaurant theNewRestaurant = _restaurantRepository.GetRestaurantByUserId(userId);
                    return RedirectToAction("Index", "Restaurant", new {id = theNewRestaurant.Id});
                }
            }
            return View(model);
            //return RedirectToAction("Index", new {id = model.Restaurant.Id});
        }

    }
}
