using System;
using Application.Restaurant.Dto;

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
        ReservationResult AddNewReservation(ReservationRequest request);
    }
}