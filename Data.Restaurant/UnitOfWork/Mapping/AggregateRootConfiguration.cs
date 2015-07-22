using System;
using System.Data.Entity.ModelConfiguration;
using Domain.Restaurant.ReservationModule.Aggregates;

namespace Infrastructure.Data.Restaurant.UnitOfWork.Mapping
{
    public class AggregateRootConfiguration : EntityTypeConfiguration<AggregateRoot>
    {
        public AggregateRootConfiguration()
        {
            Property(e => e.CreatedBy).HasMaxLength(125).IsRequired();
            Property(e => e.CreatedOn).IsRequired();
        }
    }
}
