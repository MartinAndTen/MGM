using System;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroupProjectRestaurangMVC01.Tests.Controllers
{
    [TestClass]
    public class RestaurantControllerTest
    {
        RestaurantRepository _restaurantRepository = new RestaurantRepository();

        [TestMethod]
        public void CanCreateRestaurant()
        {
            bool result = _restaurantRepository.AddOrUpdateRestaurant(CreateTestRestaurant());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanGetRestaurantById()
        {
            Restaurant result = _restaurantRepository.GetRestaurantById(new Guid("a60c9f39-35ee-4ef4-9515-53200c0b4db0"));
            Assert.IsNotNull(result);
            StringAssert.StartsWith(result.Name, Helpers.RestaurangTestHelper.TestRestaurantStrings);
        }

        [TestMethod]
        public void CanDeleteRestaurantById()
        {
            bool result = _restaurantRepository.DeleteRestaurant(Helpers.RestaurangTestHelper.TestRestaurantId);
            Assert.IsTrue(result);
        }

        private Restaurant CreateTestRestaurant()
        {
            Restaurant newRestaurant = new Restaurant();
            newRestaurant.Id = Helpers.RestaurangTestHelper.TestRestaurantId;
            newRestaurant.Name = Helpers.RestaurangTestHelper.TestRestaurantStrings;
            newRestaurant.Description = Helpers.RestaurangTestHelper.TestRestaurantStrings;
            newRestaurant.Address = Helpers.RestaurangTestHelper.TestRestaurantStrings;
            newRestaurant.Zipcode = Helpers.RestaurangTestHelper.TestRestaurantInt;
            newRestaurant.Email = Helpers.RestaurangTestHelper.TestRestaurantStrings;
            newRestaurant.Activated = true;
            return newRestaurant;
        }
    }
}
