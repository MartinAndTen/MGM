﻿using GroupProjectRestaurangMVC01.Models;
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
        public ActionResult Create(ReservationViewModel model)
        {
            _reservationRepository.CreateReservation(model);
            return View();
        }
    }
}
