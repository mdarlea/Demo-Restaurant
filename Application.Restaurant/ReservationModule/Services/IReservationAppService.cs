using System;

namespace Application.Restaurant.ReservationModule.Services
{
    /// <summary>
    /// The reservation application service methods
    /// </summary>
    public interface IReservationAppService : IDisposable
    {
        /// <summary>
        /// Creates a new reservation and saves it to the database
        /// </summary>
        /// <param name="request">Create reservation DTO</param>
        /// <returns>The newly created reserrvation DTO</returns>
        Dto.ReservationResult AddNewReservation(Dto.ReservationRequest request);

        /// <summary>
        /// Get all the reservations
        /// </summary>
        /// <returns>A collection with found reservations</returns>
        Dto.CollectionActionResult<Dto.Reservation> GetAllReservations();

        /// <summary>
        /// Get a reservation by ID
        /// </summary>
        /// <returns>The reservation with the given ID</returns>
        Dto.ReservationResult GetReservation(int id);
    }
}