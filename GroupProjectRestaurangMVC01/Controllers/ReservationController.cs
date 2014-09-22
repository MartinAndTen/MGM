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

        public ActionResult Index(Guid? id)
        {
            ReservationViewModel reservationViewModel = GetReservation();
            Restaurant currentRestaurant = new Restaurant();

            if (id.HasValue)
            {
                currentRestaurant = _restaurantRepository.GetRestaurantById(id.Value);
                IList<Table> tables = _restaurantRepository.GetRestaurantTablesById(id.Value);
                reservationViewModel.Restaurant = currentRestaurant;
                reservationViewModel.Restaurant.Tables = tables;
            }
            else if(id.HasValue == false)
            {
                //Felmedelande eller skicka anvädaren till att välja om restaurant eller nått här kanske?
            }

            return View("FirstCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstCreate(ReservationViewModel firstPartReservation, string BtnPrevious, string BtnNext, Guid? id)
        {
            if (BtnNext != null)
            {
                ReservationViewModel reservationViewModel = GetReservation();
                Restaurant currentRestaurant = reservationViewModel.Restaurant;

                reservationViewModel.Restaurant = currentRestaurant;
                reservationViewModel.TotalGuests = firstPartReservation.TotalGuests;

                if (currentRestaurant.MaxSeatPerBooking.HasValue)
                {
                    if (reservationViewModel.TotalGuests > currentRestaurant.MaxSeatPerBooking.Value)
                    {
                        
                    }
                }

                reservationViewModel.Date = firstPartReservation.Date;
                return View("SecondCreate");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SecondCreate(ReservationViewModel secondPartReservation, string BtnPrevious, string BtnNext, Guid? id)
        {
            ReservationViewModel reservationViewModel = GetReservation();
            if (BtnPrevious != null)
            {
                ReservationViewModel firstPartViewModel = new ReservationViewModel();
                firstPartViewModel.TotalGuests = reservationViewModel.TotalGuests;
                firstPartViewModel.Date = reservationViewModel.Date;
                firstPartViewModel.Restaurant = reservationViewModel.Restaurant;
                firstPartViewModel.Restaurant.Tables = reservationViewModel.Restaurant.Tables;
                return View("FirstCreate", firstPartViewModel);
            }
            if (BtnNext != null)
            {
                //reservationViewModel.TotalGuests = reservationViewModel.TotalGuests;
                //reservationViewModel.Date = reservationViewModel.Date;
                reservationViewModel.CustomerName = secondPartReservation.CustomerName;
                reservationViewModel.CustomerPhoneNumber = secondPartReservation.CustomerPhoneNumber;
                reservationViewModel.ContactEmail = secondPartReservation.ContactEmail;

                using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                {
                    if (ModelState.IsValid)
                    {
                        Reservation reservation = new Reservation();
                        reservation.CustomerName = reservationViewModel.CustomerName;
                        reservation.ContactEmail = reservationViewModel.ContactEmail;
                        reservation.CustomerPhoneNumber = reservationViewModel.CustomerPhoneNumber;
                        //reservation.Restaurant = reservationViewModel.Restaurant;
                        reservation.TotalGuests = reservationViewModel.TotalGuests;
                        reservation.RestaurantId = reservationViewModel.Restaurant.Id;
                        reservation.Date = reservationViewModel.Date;
                        reservation.EndDate = reservationViewModel.Date.AddHours(1);

                        db.Reservations.Add(reservation);
                        db.SaveChanges();
                    }
                    RemoveReservation();
                    return View("Success");
                }
            }
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(ReservationViewModel viewModel)
        //{
        //    //Skicka till nästa partial view här på nått sätt
        //    return View();
        //}

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

        public ReservationViewModel GetReservation()
        {
            if (Session["reservation"] == null)
            {
                Session["reservation"] = new ReservationViewModel();
            }
            return (ReservationViewModel)Session["reservation"];
        }

        public void RemoveReservation()
        {
            Session.Remove("reservation");
        }
    }
}
