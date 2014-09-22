using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.ViewModels;

namespace GroupProjectRestaurangMVC01.Repository
{
    public class RestaurantRepository
    {

        //Url to No-Image image//
        public string DefaultNoImageLocation = "../../Assets/Images/800px-No_Image_Wide.png";


        //Create/Update/Delete Restaurant
        public bool CreateRestaurant(int userId, string name, string description, string address, int zipcode, string phone,
           string city, int? totalSeats, int? capacity, int? maxSeatPerBooking, string rating, string photo,
           string email, bool activated)
        {
            bool resultValue = false;
            try
            {
                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    Restaurant newRestaurant = new Restaurant();
                    newRestaurant.UserId = userId;
                    newRestaurant.Id = Guid.NewGuid();
                    newRestaurant.Name = name;
                    newRestaurant.Description = description;
                    newRestaurant.Address = address;
                    newRestaurant.Zipcode = zipcode;
                    newRestaurant.Phone = phone;
                    newRestaurant.City = city;
                    if (totalSeats.HasValue)
                    {
                        newRestaurant.TotalSeats = totalSeats.Value;
                    }
                    if (capacity.HasValue)
                    {
                        newRestaurant.Capacity = capacity.Value;
                    }
                    if (maxSeatPerBooking.HasValue)
                    {
                        newRestaurant.MaxSeatPerBooking = maxSeatPerBooking.Value;
                    }
                    newRestaurant.Rating = "noll";
                    newRestaurant.Photo = photo;
                    newRestaurant.Email = email;
                    newRestaurant.Activated = activated;

                    db.Restaurants.Add(newRestaurant);
                    db.SaveChanges();
                    resultValue = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultValue;
        }
        public bool UpdateRestaurant(int userId,RestaurantViewModel restaurant, HttpPostedFileBase file)
        {
            bool returnValue = false;
            try
            {
                Restaurant restaurantToUpdate = GetRestaurantByUserId(userId);
                string photo = restaurantToUpdate.Photo;

                if (file != null)
                {
                    bool deleteImageResult;
                    if (photo != DefaultNoImageLocation)
                    {
                        deleteImageResult = DeleteThisImage(photo);
                    }
                    else
                    {
                        deleteImageResult = true;
                    }
                    if (deleteImageResult)
                    {
                        var result = UploadImageToRestaurant(file);
                        photo = result.Photo;
                    }
                }

                restaurantToUpdate.Name = restaurant.Name;
                restaurantToUpdate.Description = restaurant.Description;
                restaurantToUpdate.Address = restaurant.Address;
                restaurantToUpdate.Zipcode = restaurant.Zipcode;
                restaurantToUpdate.Phone = restaurant.Phone;
                restaurantToUpdate.City = restaurant.City;
                restaurantToUpdate.TotalSeats = restaurant.TotalSeats;
                restaurantToUpdate.Capacity = restaurant.Capacity;
                restaurantToUpdate.Photo = photo;
                restaurantToUpdate.Email = restaurant.Email;
                restaurantToUpdate.Activated = restaurant.Activated;

                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    db.Restaurants.AddOrUpdate(restaurantToUpdate);
                    db.SaveChanges();
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public bool DeleteRestaurant(Guid restaurantId)
        {
            bool returnValue = false;
            try
            {
                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    Restaurant restaurantToDelete = db.Restaurants.FirstOrDefault(c => c.Id.Equals(restaurantId));
                    if (restaurantToDelete != null)
                    {
                        db.Restaurants.Remove(restaurantToDelete);
                        db.SaveChanges();
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }


        //Get Restaurant from db
        public List<Table> GetRestaurantTablesById(Guid id)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                List<Table> tables = db.Tables.Include("ReservedTables").Where(c => c.RestaurantId.Equals(id)).ToList();
                return tables;
            }
        }

        public Restaurant GetRestaurantById(Guid id)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Restaurant result = db.Restaurants.Include("ClosedForBookings")
                    .Include("OpenForBookings")
                    .Include("Reservations")
                    .Include("Tables").Include("Tables.ReservedTables")
                    .FirstOrDefault(c => c.Id.Equals(id));
                return result;
            }
        }

        public Restaurant GetRestaurantByUserId(int id)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Restaurant result = db.Restaurants.Include("ClosedForBookings")
                    .Include("OpenForBookings")
                    .Include("Reservations")
                    .Include("Tables").Include("Tables.ReservedTables").FirstOrDefault(c => c.UserId.Equals(id));
                return result;
            }
        }

        public List<Restaurant> GetAllRestaurantsToList()
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                List<Restaurant> result = db.Restaurants.Include("ClosedForBookings")
                    .Include("OpenForBookings")
                    .Include("Reservations")
                    .Include("Tables").Include("Tables.ReservedTables").ToList();
                return result;
            }
        }

        //Upload/Delete Image To/From Restaurant 
        public Restaurant UploadImageToRestaurant(HttpPostedFileBase file)
        {
            string fileExtention = Path.GetExtension(file.FileName);
            //Creating filename to avoid filename conflicts
            string filename = Guid.NewGuid().ToString();
            string pic = filename + fileExtention;
            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Assets/UserAssets/Images"), pic);
            Restaurant restaurantPhotoUrl = new Restaurant();
            file.SaveAs(path);
            restaurantPhotoUrl.Photo = "../../Assets/UserAssets/Images/"+Path.GetFileName(path);
            return restaurantPhotoUrl;
        }

        public bool DeleteThisImage(string imageString)
        {
            bool returnValue = false;
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath(imageString);
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return returnValue;
        }
        
        //Add Restaurant from model props to ViewModel props
        public RestaurantViewModel AddRestaurantToViewModel(Restaurant restaurantToEdit)
        {
            RestaurantViewModel viewModel = new RestaurantViewModel();
            viewModel.Restaurant = restaurantToEdit;
            viewModel.Name = restaurantToEdit.Name;
            viewModel.Description = restaurantToEdit.Description;
            viewModel.Address = restaurantToEdit.Address;
            viewModel.Zipcode = restaurantToEdit.Zipcode;
            viewModel.Phone = restaurantToEdit.Phone;
            viewModel.Photo = restaurantToEdit.Photo;
            viewModel.City = restaurantToEdit.City;
            viewModel.TotalSeats = restaurantToEdit.TotalSeats;
            viewModel.Capacity = restaurantToEdit.Capacity;
            viewModel.MaxSeatPerBooking = restaurantToEdit.MaxSeatPerBooking;
            viewModel.Email = restaurantToEdit.Email;
            viewModel.Activated = restaurantToEdit.Activated;

            return viewModel;
        }
        ///
        /// 
        /// 
        /// 
        /// 
        //Table Create/Edit/Delete
        public bool CreateTable(int userId,TableViewModel table)
        {
            bool returnValue = false;
            try
            {
                Restaurant userRestaurant = GetRestaurantByUserId(userId);
                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    Table newTable = new Table();
                    newTable.RestaurantId = userRestaurant.Id;
                    newTable.TableName = table.TableName;
                    newTable.Seats = table.Seats;
                    db.Tables.Add(newTable);
                    db.SaveChanges();
                    returnValue = true;
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public bool DeleteTable(int userId, int id)
        {
            bool returnValue = false;
            try
            {
                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    Table tableToDelete = new Table();
                    if (tableToDelete != null)
                    {
                        db.Tables.Remove(tableToDelete);
                        db.SaveChanges();
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }
        ///
        /// 
        /// Get Tables
        public Table GetTableById(int id)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Table table = db.Tables.Include("ReservedTables").FirstOrDefault(c => c.Id.Equals(id));
                return table;
            }
        }
    }
}