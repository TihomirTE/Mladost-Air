    using MladostAir.Models;
using MladostAir.Models.Enum;
using System.Data.Entity.Migrations;

namespace MladostAir.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MladostAirDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;

            this.ContextKey = "MladostAir.Data.MladostAirDbContext";
        }

        // set initial data
        protected override void Seed(MladostAirDbContext context)
        {
            //context.Countries
            //  .AddOrUpdate(
            //  d => d.Name,
            //  new Country
            //  {
            //      Name = "Bulgaria"
            //  });

            //context.Cities
            //   .AddOrUpdate(
            //   d => d.Name,
            //   new City
            //   {
            //       Name = "Sofia"
            //   });

            //context.Airports
            //  .AddOrUpdate(
            //  d => d.Name,
            //  new Airport
            //  {
            //      Name = "Sofia",
            //      AirportCode = "LBSF"
            //  });

            //context.Airlines
            // .AddOrUpdate(
            // d => d.Name,
            // new Airline
            // {
            //     Name = "Bulgar"
            // });

            //context.Customers
            //    .AddOrUpdate(
            //    c => c.FirstName,
            //    new Customer
            //    {
            //        FirstName = "Pesho",
            //        LastName = "Peshev",
            //        CustomerNumber = 123,
            //        Age = 20
            //    });

            //context.Tickets
            //    .AddOrUpdate(
            //    t => t.TicketNumber,
            //    new Ticket
            //    {
            //        TicketNumber = 1234,
            //        TypeClass = TypeClass.Bussiness,
            //        TypeAircraft = TypeAircraft.Airbus320
            //    });
        }
    }
}
