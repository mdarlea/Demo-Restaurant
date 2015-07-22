using System;

namespace Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg
{
    /// <summary>
    /// Creates a new reservation
    /// </summary>
    public static class ReservationFactory
    {
        public static Reservation CreateReservation(string name, DateTime reservationDateTime, int guestsCount, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (guestsCount < 1) throw new ArgumentOutOfRangeException("guestsCount");
            if (string.IsNullOrWhiteSpace(createdBy)) throw new ArgumentNullException("createdBy");

            //creates a new reservation entity
            var reservation = new Reservation
            {
                Name = name,
                ReservationDateTime = reservationDateTime,
                GuestsCount = guestsCount,
                IsDeleted = false,
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now
            };

            return reservation;
        }
    }
}
