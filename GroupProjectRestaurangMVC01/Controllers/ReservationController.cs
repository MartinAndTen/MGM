﻿using System.Web.UI.WebControls;
using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.Repository;
using GroupProjectRestaurangMVC01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Table = GroupProjectRestaurangMVC01.Models.Table;
using Postal;

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
                reservationViewModel.Result = string.Empty;
            }
            else if (id.HasValue == false)
            {
                reservationViewModel.Result = "Could not find which restaurant you selected, please go back and try again!";
            }
            return View("FirstCreate", reservationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstCreate(ReservationViewModel firstPartReservation, string BtnPrevious, string BtnNext, Guid? id)
        {
            if (BtnNext != null)
            {
                ReservationViewModel reservationViewModel = GetReservation();
                Restaurant currentRestaurant = reservationViewModel.Restaurant;
                List<Reservation> RestaurantsReservations =
                        _reservationRepository.GetAllReservationsFromRestaurantId(currentRestaurant.Id, firstPartReservation.Date);
                int currentRestaurantTotalGuests = 0;
                foreach (var restaurantsReservation in RestaurantsReservations)
                {
                    currentRestaurantTotalGuests += restaurantsReservation.TotalGuests;
                }

                currentRestaurantTotalGuests += firstPartReservation.TotalGuests;

                if (currentRestaurant.Capacity <= currentRestaurantTotalGuests)
                {
                    firstPartReservation.Result = "sry mange to many people this day already";
                    return View("FirstCreate", firstPartReservation);
                }

                reservationViewModel.TotalGuests = firstPartReservation.TotalGuests;
                if (currentRestaurant.MaxSeatPerBooking.HasValue)
                {
                    if (reservationViewModel.TotalGuests > currentRestaurant.MaxSeatPerBooking.Value)
                    {
                        firstPartReservation.Result = currentRestaurant.Name + " allows a maximum of " + currentRestaurant.MaxSeatPerBooking + " persons per reservation";
                        return View("FirstCreate", firstPartReservation);
                    }
                }
                reservationViewModel.Date = firstPartReservation.Date;
                string dayOfWeek = reservationViewModel.Date.DayOfWeek.ToString();
                var currentRestaurantOpeningTimes = currentRestaurant.OpenForBookings.Where(c => c.RestaurantId == currentRestaurant.Id);
                var currentDayOfWeekOpenTimes = currentRestaurantOpeningTimes.FirstOrDefault(c => c.Day == dayOfWeek);
                if (currentDayOfWeekOpenTimes != null)
                {
                    reservationViewModel.openTime = Convert.ToDateTime(currentDayOfWeekOpenTimes.StartTime.ToString());
                    reservationViewModel.closeTime = Convert.ToDateTime(currentDayOfWeekOpenTimes.EndTime.ToString());
                }
                else
                {
                    reservationViewModel.Result = "This restaurant is not open for online booking this date";
                    return View("FirstCreate", reservationViewModel);
                }
                TimeSpan totalOpeningHours = reservationViewModel.closeTime.TimeOfDay - reservationViewModel.openTime.TimeOfDay;
                reservationViewModel.ammountOfButtonsToGenerate = (int)totalOpeningHours.TotalHours * 2 - 1;
                var ButtonStartTime = reservationViewModel.openTime;
                List<string> tempButtonList = new List<string>();
                Dictionary<string, DateTime> buttonDictionary = new Dictionary<string, DateTime>();
                for (int i = 0; i < reservationViewModel.ammountOfButtonsToGenerate; i++)
                {
                    tempButtonList.Add(ButtonStartTime.ToString("HH:mm"));
                    ButtonStartTime = ButtonStartTime.AddMinutes(30);
                }
                reservationViewModel.ButtonList = tempButtonList;
                List<Table> tableList = new List<Table>();
                List<ReservedTable> reservedTables = new List<ReservedTable>();
                //using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
                //{
                //Lista över tables som har tillräckligt med platser för bokningen på den specifika restuarangen man ska gör en bokning på
                tableList = _reservationRepository.GetTablesWithEnoughSeatsByRestaurantId(reservationViewModel, tableList);
                reservationViewModel.BokBaraTables = tableList;
                //vet inte varför men vi behövde göra såhär
                List<string> tempButtonList2 = new List<string>();
                foreach (var tempButton in tempButtonList)
                {
                    tempButtonList2.Add(tempButton);
                }
                List<ReservedTable> newTable = new List<ReservedTable>();
                List<ReservedTable> newTables2 = new List<ReservedTable>();
                foreach (var reservation in RestaurantsReservations)
                {
                    foreach (var table in tableList)
                    {
                        newTable = _reservationRepository.GetReservedTablesByTableId(newTable, table);
                        if (newTable.Count != 0)
                        {
                            foreach (var tables in newTable)
                            {
                                if (newTable != newTables2)
                                {
                                    newTables2.Add(tables);
                                }
                            }
                        }
                    }
                }
                reservedTables = newTables2;
                List<string> buttonsToRemoveList = new List<string>();
                if (reservedTables.Count != 0)
                {
                    foreach (var table in reservedTables)
                    {
                        buttonsToRemoveList.Add(table.StartDate.ToString("HH:mm"));
                    }
                    foreach (var item in tempButtonList2)
                    {
                        foreach (var button in buttonsToRemoveList)
                        {
                            if (button == item)
                            {
                                TimeSpan item2 = TimeSpan.Parse(item);
                                TimeSpan item3 = new TimeSpan(0, 30, 0);
                                item2 = item2.Add(item3);
                                DateTime dateItem2 = new DateTime(item2.Ticks);
                                string finalItem = dateItem2.ToString("HH:mm");
                                reservationViewModel.ButtonList.Remove(item);
                                reservationViewModel.ButtonList.Remove(finalItem);
                            }
                        }
                    }
                }
                //}
                return View("SecondCreate", reservationViewModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SecondCreate(ReservationViewModel secondPartReservation, string BtnPrevious, string BtnNext, Guid? id)
        {
            ReservationViewModel reservationViewModel = GetReservation();
            Restaurant currentRestaurant = reservationViewModel.Restaurant;

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
                reservationViewModel.CustomerName = secondPartReservation.CustomerName;
                reservationViewModel.CustomerPhoneNumber = secondPartReservation.CustomerPhoneNumber;
                reservationViewModel.ContactEmail = secondPartReservation.ContactEmail;
                reservationViewModel.Date = reservationViewModel.Date.Add(TimeSpan.Parse(secondPartReservation.TimeString));
                if (ModelState.IsValid)
                {
                    _reservationRepository.SaveReservationToDB(reservationViewModel);
                }
                RemoveReservation();
                return View("Success");

            }
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
