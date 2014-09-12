using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.Repository;
using GroupProjectRestaurangMVC01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProjectRestaurangMVC01.Controllers
{
    public class ReservationController : Controller
    {
        //
        // GET: /Booking/
        private readonly ReservationRepository _reservationRepository = new ReservationRepository();
        public ActionResult Index(int? id)
        {
            ReservationViewModel viewModel = new ReservationViewModel();
            if (id.HasValue)
            {
                var id2 = (int)id;
                Reservation reservation;
                reservation = _reservationRepository.GetReservationsByID(id2);
                if (reservation != null)
                {
                    viewModel.Reservation = reservation;
                }
            }
            else
            {
                viewModel.Reservations = _reservationRepository.GetAllReservationsToList();
            }
            
            return View(viewModel);
        }

        public ActionResult Create(RestaurantViewModel model)
        {
            return View();
        }

    }
}
