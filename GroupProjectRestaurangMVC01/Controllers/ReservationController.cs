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
        private ReservationRepository _reservationRepository;
        public ActionResult Index(int? id)
        {
            ReservationViewModel viewModel = new ReservationViewModel();
            if (id.HasValue)
            {
                var id2 = (int)id;
                viewModel.Reservation = _reservationRepository.GetReservationsByID(id2);
            }
            else
            {
                viewModel.Reservations = _reservationRepository.GetAllReservationsToList();
            }
            
            return View(viewModel);
        }

    }
}
