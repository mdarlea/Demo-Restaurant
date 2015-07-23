using System;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Core.Dto;
using Swaksoft.Infrastructure.Crosscutting.Extensions;
using Swaksoft.Infrastructure.Crosscutting.Validation;

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

                var entityValidator = EntityValidatorLocator.CreateValidator();
                if (entityValidator.IsValid(reservation))
                {
                    using (var transaction = _reservationRepository.BeginTransaction())
                    {
                        _reservationRepository.Add(reservation);
                        transaction.Commit();
                    }
                }
                else
                {
                    return new Dto.ReservationResult
                    {
                        Status = ActionResultCode.Errored,
                        Message = Messages.validation_errors,
                        Errors = entityValidator.GetInvalidMessages(reservation)
                    };
                }
                
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
        /// <param name="id">The reservation id</param>
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
                        Message = string.Format(Messages.reservation_not_found, id)
                    };
                }
                return reservation.ProjectedAs<Dto.ReservationResult>();
            });
        }

        /// <summary>
        /// Updates an existing reservation
        /// </summary>
        /// <param name="id">The reservation id</param>
        /// <param name="request">The changed reservation DTO</param>
        /// <returns>The updated reservation DTO</returns>
        public Dto.ReservationResult UpdateReservation(int id, Dto.ReservationRequest request)
        {
            if (id < 1) throw new ArgumentOutOfRangeException("id");

            return Call(() =>
            {
                using (var transaction = _reservationRepository.BeginTransaction())
                {
                    //get the reservation
                    var reservation = _reservationRepository.Get(id);

                    //returns failure if a reservation is not found
                    if (reservation == null)
                    {
                        return new Dto.ReservationResult
                        {
                            Status = ActionResultCode.Failed,
                            Message = string.Format(Messages.reservation_not_found, id)
                        };
                    }

                    //updates fields from the DTO
                    reservation.Name = request.Name;
                    reservation.ReservationDateTime = request.ReservationDateTime;
                    reservation.GuestsCount = request.GuestsCount;
                    reservation.ModifiedBy = request.UserId;
                    reservation.ModifiedOn = DateTime.Now;

                    //validates the reservation
                    var entityValidator = EntityValidatorLocator.CreateValidator();
                    if (entityValidator.IsValid(reservation))
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        return new Dto.ReservationResult
                        {
                            Status = ActionResultCode.Errored,
                            Message = Messages.validation_errors,
                            Errors = entityValidator.GetInvalidMessages(reservation)
                        };
                    }

                    //returns the updated reservation
                    return reservation.ProjectedAs<Dto.ReservationResult>();
                }
            });
        }

        /// <summary>
        /// Marks a reservation as deleted
        /// </summary>
        /// <param name="id">The id of the reservation that must be removed</param>
        /// <returns></returns>
        public ActionResult DeleteReservation(int id)
        {
            if (id < 1) throw new ArgumentOutOfRangeException("id");

            return Call(() =>
            {
                //returns failure if a reservation is not found
                using (var transaction = _reservationRepository.BeginTransaction())
                {
                    //get the reservation
                    var reservation = _reservationRepository.Get(id);

                    //returns failure if a reservation is not found
                    if (reservation == null)
                    {
                        return new Dto.ReservationResult
                        {
                            Status = ActionResultCode.Failed,
                            Message = string.Format(Messages.reservation_not_found, id)
                        };
                    }

                    //mark this reservation as deleted
                    reservation.IsDeleted = true;
                    
                    transaction.Commit();
                }
                
                //return success
                return new ActionResult
                {
                    Status = ActionResultCode.Success
                };
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
