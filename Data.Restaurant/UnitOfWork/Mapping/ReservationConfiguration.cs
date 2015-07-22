using System;
using System.Data.Entity.ModelConfiguration;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;

namespace Infrastructure.Data.Restaurant.UnitOfWork.Mapping
{
    public class ReservationConfiguration : EntityTypeConfiguration<Reservation>
    {
        public ReservationConfiguration()
        {
            Property(e => e.Name).HasMaxLength(125).IsRequired();
            Property(e => e.ReservationDateTime).IsRequired();
            Property(e => e.GuestsCount).IsRequired();

            Map(map =>
            {
                map.MapInheritedProperties();
                map.ToTable("Reservations");
            });
        }
    }
}
