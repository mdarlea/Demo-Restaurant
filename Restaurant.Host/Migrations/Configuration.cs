using System.Collections.Generic;
using Swaksoft.Core.Crypto;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;

namespace Restaurant.Host.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Restaurant.Host.Authorization.ApplicationUserDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Restaurant.Host.Authorization.ApplicationUserDbContext context)
        {
            context.Clients.AddOrUpdate(c => c.Id, BuildClientsList().ToArray());
            context.SaveChanges();
        }

        private static List<Client> BuildClientsList()
        {

            var clientsList = new List<Client> 
            {
                new Client
                { 
                    Id = "RestaurantDemoApp", 
                    Secret= CryptoAes.GetHash("mdarlea@gmail.com"), 
                    Name="Chinese Restaurant", 
                    ApplicationType =  ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "http://www.swaksoft.com"
                },
                new Client
                { 
                    Id = "RestaurantDemoTestApp", 
                    Secret= CryptoAes.GetHash("mdarlea@gmail.com"), 
                    Name="Chinese Restaurant Test", 
                    ApplicationType =  ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "http://localhost:20178"
                }
            };

            return clientsList;
        }
    }
}
