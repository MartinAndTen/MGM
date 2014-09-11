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
        public ActionResult Index()
        {
            ReservationViewModel viewModel = new ReservationViewModel();

            viewModel.Reservations = _reservationRepository.GetAllReservationsToList();
            return View(viewModel);
        }

    }
}
