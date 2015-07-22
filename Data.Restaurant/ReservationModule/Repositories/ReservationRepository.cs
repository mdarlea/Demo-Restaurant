using System;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Infrastructure.Data.Restaurant.ReservationModule.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(Swaksoft.Domain.Seedwork.ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
