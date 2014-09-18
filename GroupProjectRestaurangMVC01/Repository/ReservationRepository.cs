using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        public Reservation CreateReservation(ReservationViewModel viewModel)
        {
            Reservation reservation = new Reservation();
            using (RestaurantProjectMVC01Entities db =  new RestaurantProjectMVC01Entities())
            {
                reservation.Day = viewModel.Day;
                reservation.CustomerName = viewModel.CustomerName;
                reservation.ContactEmail = viewModel.ContactEmail;
                reservation.CustomerPhoneNumber = viewModel.CustomerPhoneNumber;
                reservation.Date = viewModel.Date;
                reservation.RestaurantId = viewModel.Restaurant.Id;
                //reservation.TableId = viewModel.Tables
            }

            return reservation;
        }
    }
}