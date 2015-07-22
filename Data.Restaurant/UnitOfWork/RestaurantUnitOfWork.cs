using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Restaurant.ReservationModule.Aggregates.ReservationAgg;
using Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork;

namespace Infrastructure.Data.Restaurant.UnitOfWork
{
    public abstract class RestaurantUnitOfWork : EntityFrameworkUnitOfWork
    {
        protected RestaurantUnitOfWork()
            : base("RestaurantDataSource")
        {
        }

        protected RestaurantUnitOfWork(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
        }
    }
}
