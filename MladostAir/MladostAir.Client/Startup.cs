using ExternalFiles.Readers;
using MladostAir.Data;
using MladostAir.Data.Migrations;
using MladostAir.Models;
using MladostAir.Models.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace MladostAir.Client
{
    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MladostAirDbContext, Configuration>());

            var db = new MladostAirDbContext();



            //            var json = @"{
            //    'Name': 'EasyJet',
            //    'TypeAircraft': 'Boing747'
            //}";
            var jsonReader = new JsonReportFileReader();
            var json = jsonReader.ReadJsonReports();
            var airline2 = JsonConvert.DeserializeObject<Airline>(json);

            var destination = new Destination
            {
                Name = "Plovdiv",
            };

            var country = new Country
            {
                Name = "Bulgaria"
            };

            var airport = new Airport
            {
                Name = "Plovdiv",
                AirportCode = "LBPD"
            };

            var airline = new Airline
            {
                Name = airline2.Name,
                TypeAircraft = airline2.TypeAircraft
            };

            var customer = new Customer
            {
                FirstName = "Stoqn",
                LastName = "Stoqnov",
                CustomerNumber = 4321,
                Age = 14
            };

            var ticket = new Ticket
            {
                TicketNumber = 9876,
                TypeClass = TypeClass.Economy,
                CustomerId = 2,
                DestinationId = 2
            };

            db.Destinations.Add(destination);
            db.Countries.Add(country);
            db.Airports.Add(airport);
            db.Airlines.Add(airline);
            db.Customers.Add(customer);
            db.Tickets.Add(ticket);
            db.SaveChanges();

            //Console.WriteLine(db.Destinations.Count());
        }
    }
}
