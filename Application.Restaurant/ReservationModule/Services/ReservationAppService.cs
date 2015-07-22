using System;
using Application.Restaurant.Dto;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Application.Restaurant.ReservationModule.Services
{
    public class ReservationAppService : AppServiceBase<ReservationAppService>, IReservationAppService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationAppService(IReservationRepository reservationRepository)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            _reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Creates a new reservation and saves it to the database
        /// </summary>
        /// <param name="request">Create reservation DTO</param>
        /// <returns>The newly created reserrvation DTO</returns>
        public ReservationResult AddNewReservation(ReservationRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            return Call(() =>
            {
                //creates a new reservation entity
                var reservation = ReservationFactory.CreateReservation(request.Name, request.ReservationDateTime,request.GuestsCount, request.UserId);
                _reservationRepository.SaveEntity(reservation);

                //returns success
                return reservation.ProjectedAs<ReservationResult>();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            _reservationRepository.Dispose();
        }

    }
}
