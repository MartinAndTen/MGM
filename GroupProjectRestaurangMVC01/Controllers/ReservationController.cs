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
        private readonly RestaurantRepository _restaurantRepository = new RestaurantRepository();

        public ActionResult Index()
        {
            //ReservationViewModel viewModel = new ReservationViewModel();
            //if (id.HasValue)
            //{
            //    Guid _id = (Guid)id;
            //    Restaurant currentRestaurant = _restaurantRepository.GetRestaurantById(_id);
            //}

            return View("FirstCreate");
        }

        private ReservationViewModel GetReservation()
        {
            if (Session["reservation"] == null)
            {
                Session["reservation"] = new ReservationViewModel();
            }
            return (ReservationViewModel)Session["reservation"];
        }

        private void RemoveReservation()
        {
            Session.Remove("reservation");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstCreate(ReservationViewModel firstPartReservation, string BtnPrevious, string BtnNext, Guid? id)
        {
            if (BtnNext != null)
            {
                //if (ModelState.IsValid)
                //{
                ReservationViewModel reservationViewModel = GetReservation();
                var id2 = (Guid)id;
                Restaurant currentRestaurant = _restaurantRepository.GetRestaurantById(id2);
                List<Table> tables = _restaurantRepository.GetRestaurantTablesById(id2);
                reservationViewModel.Restaurant = currentRestaurant;
                reservationViewModel.Tables = tables;
                reservationViewModel.TotalGuests = firstPartReservation.TotalGuests;
                reservationViewModel.Date = firstPartReservation.Date;
                return View("SecondCreate");
                //}
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SecondCreate(ReservationViewModel secondPartReservation, string BtnPrevious, string BtnNext, Guid id)
        {
            ReservationViewModel reservationViewModel = GetReservation();
            if (BtnPrevious != null)
            {
                //CricketerDetails DetailsObj = new CricketerDetails();
                ReservationViewModel firstPartViewModel = new ReservationViewModel();
                firstPartViewModel.TotalGuests = reservationViewModel.TotalGuests;
                firstPartViewModel.Date = reservationViewModel.Date;
                //Reservation firstPartReservation = new Reservation();
                //firstPartReservation.TotalGuests = reservation.TotalGuests;
                //firstPartReservation.Date = reservation.Date;
                return View("FirstCreate", firstPartViewModel);
            }

            if (BtnNext != null)
            {
                //if (ModelState.IsValid)
                //{
                //reservationViewModel.TotalGuests = secondPartReservation.TotalGuests;
                //reservationViewModel.Date = secondPartReservation.Date;
                reservationViewModel.Day = "Monday";
                reservationViewModel.CustomerName = "kurtan";
                reservationViewModel.CustomerPhoneNumber = "12345689";
                reservationViewModel.ContactEmail = "hej@hej.se";
                reservationViewModel.ConfirmedReservation = false;
                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    //db.Reservations.Add);
                    //db.SaveChanges();
                    RemoveReservation();
                    return View("Success");
                }
                //}
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ReservationViewModel viewModel)
        {
            //Skicka till nästa partial view här på nått sätt
            return View();
        }

        public ActionResult Create(Guid id)
        {
            ReservationViewModel viewModel = new ReservationViewModel();
            Restaurant currentRestaurant = _restaurantRepository.GetRestaurantById(id);
            List<Table> tables = _restaurantRepository.GetRestaurantTablesById(id);
            viewModel.Restaurant = currentRestaurant;
            viewModel.Tables = tables;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModel viewModel)
        {
            _reservationRepository.CreateReservation(viewModel);
            return View();
        }
    }
}
