using GroupProjectRestaurangMVC01.Models;
using GroupProjectRestaurangMVC01.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Postal;

namespace GroupProjectRestaurangMVC01.Repository
{
    public class ReservationRepository
    {
        public List<ReservedTable> GetReservedTablesByTableId(List<ReservedTable> newTable, Table table)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                newTable = db.ReservedTables.Where(c => c.TableId.Equals(table.Id)).ToList();
            }
            return newTable;
        }

        public List<Table> GetTablesWithEnoughSeatsByRestaurantId(ReservationViewModel reservationViewModel, List<Table> tableList)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                tableList = db.Tables.Where(c => c.RestaurantId.Equals(reservationViewModel.Restaurant.Id) && c.Seats >= reservationViewModel.TotalGuests).OrderBy(c => c.Seats).ToList();
            }
            return tableList;
        }

        public void SaveReservationToDB(ReservationViewModel reservationViewModel)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                Reservation reservation = new Reservation();
                reservation.Id = Guid.NewGuid();
                reservation.CustomerName = reservationViewModel.CustomerName;
                reservation.ContactEmail = reservationViewModel.ContactEmail;
                reservation.CustomerPhoneNumber = reservationViewModel.CustomerPhoneNumber;
                reservation.TotalGuests = reservationViewModel.TotalGuests;
                reservation.RestaurantId = reservationViewModel.Restaurant.Id;
                reservation.Date = reservationViewModel.Date;
                reservation.EndDate = reservationViewModel.Date.AddHours(1);
                db.Reservations.Add(reservation);
                db.SaveChanges();
                ReservedTable reservedTable = new ReservedTable();
                reservedTable.ReservationId = reservation.Id;
                reservedTable.StartDate = reservationViewModel.Date;
                reservedTable.EndDate = reservationViewModel.Date.AddHours(1);
                reservedTable.TableId = reservationViewModel.BokBaraTables.First().Id;
                db.ReservedTables.Add(reservedTable);
                db.SaveChanges();
                dynamic email = new Email("BookingEmail");
                email.To = reservation.ContactEmail;
                email.Send();
            }
        }

        public Reservation GetReservationsByID(Guid id)
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

        public List<Reservation> GetAllReservationsFromRestaurantId(Guid id, DateTime date)
        {
            using (RestaurantProjectMVC01Entities db = new RestaurantProjectMVC01Entities())
            {
                List<Reservation> result = db.Reservations.Where(c => c.RestaurantId.Equals(id)).ToList();
                if (result.Count != 0)
                {
                    foreach (var reservation in result)
                    {
                        if (reservation.Date.Date != date.Date)
                        {
                            result.Remove(reservation);
                        }
                    }
                }
                return result;
            }
        }
    }
}