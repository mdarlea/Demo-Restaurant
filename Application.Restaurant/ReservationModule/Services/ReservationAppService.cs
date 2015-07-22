using System;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Core.Dto;
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
        public Dto.ReservationResult AddNewReservation(Dto.ReservationRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            return Call(() =>
            {
                //creates a new reservation entity
                var reservation = ReservationFactory.CreateReservation(request.Name, request.ReservationDateTime, request.GuestsCount, request.UserId);
                _reservationRepository.SaveEntity(reservation);

                //returns success
                return reservation.ProjectedAs<Dto.ReservationResult>();
            });
        }

        /// <summary>
        /// Get all the reservations
        /// </summary>
        /// <returns>A collection with found reservations</returns>
        public Dto.CollectionActionResult<Dto.Reservation> GetAllReservations()
        {
            return Call(() =>
            {
                var spec = ReservationSpecifications.GetAll();
                var reservations = _reservationRepository.AllMatching(spec);

                return new Dto.CollectionActionResult<Dto.Reservation>
                {
                    Status = ActionResultCode.Success,
                    Items = reservations.ProjectedAsCollection<Dto.Reservation>()
                };
            });
        }

        /// <summary>
        /// Get a reservation by ID
        /// </summary>
        /// <returns>The reservation with the given ID</returns>
        public Dto.ReservationResult GetReservation(int id)
        {
            if (id < 1) throw new ArgumentOutOfRangeException("id");

            return Call(() =>
            {
                var reservation = _reservationRepository.Get(id);

                //returns failure if a reservation is not found
                if (reservation == null)
                {
                    return new Dto.ReservationResult
                    {
                        Status = ActionResultCode.Failed,
                        Message = string.Format("Could not find a reservation with the {0} id", id)
                    };
                }
                return reservation.ProjectedAs<Dto.ReservationResult>();
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
