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

        [TestInitialize]
        public void SetUp()
        {
            _restaurantRepository.AddOrUpdateRestaurant(CreateTestRestaurant());
        }

        [TestCleanup]
        public void CleanUp()
        {
            _restaurantRepository.DeleteRestaurant(Helpers.RestaurangTestHelper.TestRestaurantId);
        }

        [TestMethod]
        public void CanCreateRestaurant()
        {
            _restaurantRepository.DeleteRestaurant(Helpers.RestaurangTestHelper.TestRestaurantId);

            bool result = _restaurantRepository.AddOrUpdateRestaurant(CreateTestRestaurant());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanGetRestaurantById()
        {
            Restaurant result = _restaurantRepository.GetRestaurantById(Helpers.RestaurangTestHelper.TestRestaurantId);
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
            newRestaurant.UserId = 1;
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
