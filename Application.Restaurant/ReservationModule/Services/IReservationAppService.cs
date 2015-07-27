using System;
using Swaksoft.Core.Dto;

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
        Dto.CollectionActionResult<Dto.ReservationRequest> GetAllReservations();

        /// <summary>
        /// Get a reservation by ID
        /// </summary>
        /// <param name="id">The reservation id</param>
        /// <returns>The reservation with the given ID</returns>
        Dto.ReservationResult GetReservation(int id);
        
        /// <summary>
        /// Updates an existing reservation
        /// </summary>
        /// <param name="id">The reservation id</param>
        /// <param name="request">The changed reservation DTO</param>
        /// <returns>The updated reservation DTO</returns>
        Dto.ReservationResult UpdateReservation(int id, Dto.ReservationRequest request);

        /// <summary>
        /// Marks a reservation as deleted
        /// </summary>
        /// <param name="id">The id of the reservation that must be removed</param>
        /// <returns></returns>
        ActionResult DeleteReservation(int id);
    }
}