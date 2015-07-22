using System;
using Swaksoft.Domain.Seedwork.Specification;

namespace Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg
{
    /// <summary>
    /// Reservations specifications
    /// </summary>
    public static class ReservationSpecifications
    {
        public static ISpecification<Reservation> GetAll()
        {
            return new DirectSpecification<Reservation>(e => !e.IsDeleted);
        }
    }
}
