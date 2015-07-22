using System;
using System.Data.Entity;

namespace Infrastructure.Data.Restaurant.UnitOfWork
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class RestaurantUnitOfWorkMySql : RestaurantUnitOfWork
    {
        public RestaurantUnitOfWorkMySql()
        {
        }

        public RestaurantUnitOfWorkMySql(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}
