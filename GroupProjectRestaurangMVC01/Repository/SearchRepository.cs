using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.ViewModels;

namespace GroupProjectRestaurangMVC01.Repository
{
    public class SearchRepository
    {
        public RestaurantViewModel SearchResult(RestaurantViewModel model)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                List<Restaurant> searchResult = new List<Restaurant>();

                //Söker RestaurantName
                if (model.SearchName != null && model.SearchCity == null)
                {

                    searchResult =
                        db.Restaurants.Where(c => c.Name.Contains(model.SearchName)).ToList();
                    model.Restaurants = searchResult;
                    //Om inget hittas
                    if (searchResult.Count == 0)
                    {
                       model.Result =
                            "No Restaurants With this name was found. Please try another name or correct your spelling";

                    }

                }
                    //Söker City
                else if (model.SearchCity != null && model.SearchName == null)
                {
                    searchResult =
                        db.Restaurants.Where(c => c.City.Contains(model.SearchCity)).ToList();
                    model.Restaurants = searchResult;
                    //Om inget hittas
                    if (searchResult.Count == 0)
                    {
                        model.Result =
                            "No Restaurant in this city was found. Please try another city or correct your spelling";

                    }
                }
                    //Söker båda
                else if (model.SearchName != null && model.SearchCity != null)
                {
                    searchResult =
                        db.Restaurants.Where(c => c.Name.Contains(model.SearchName) &&
                                                  c.City.Contains(model.SearchCity)).ToList();
                    model.Restaurants = searchResult;
                    //Om inget hittas
                    if (searchResult.Count == 0)
                    {
                        model.Result =
                            "No Restaurants with this name in this city was found. Please try a with only one parameter, or correct your spelling";

                    }
                }
                    //om båda är tomma visas alla
                else if (model.SearchName == null && model.SearchCity == null)
                {
                    searchResult =
                        db.Restaurants.ToList();
                    model.Restaurants = searchResult;
                }



            }
            return model;
        }
    }
}
