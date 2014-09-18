using GroupProjectRestaurangMVC01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProjectRestaurangMVC01.Repository
{
    public class ReservationRepository
    {
        public Reservation GetReservationsByID(int id)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Reservation result = db.Reservations.FirstOrDefault(c => c.Id.Equals(id));
                return result;
            }
        }

        public List<Reservation> GetAllReservationsToList()
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                List<Reservation> result = db.Reservations.ToList();
                return result;
            }
        }

        public Reservation CreateReservation(Guid restaurantId, int tableId, DateTime date, string day, string customerName, string customerPhoneNumber, string ContactEmail)
        {
            Reservation reservation = new Reservation();

        }
    }
}