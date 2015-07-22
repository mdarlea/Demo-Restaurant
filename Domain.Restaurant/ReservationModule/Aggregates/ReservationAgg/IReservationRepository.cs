using System;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg
{
    /// <summary>
    /// Reservation repository interface
    /// </summary>
    public interface IReservationRepository : IRepository<Reservation>
    {
    }
}
