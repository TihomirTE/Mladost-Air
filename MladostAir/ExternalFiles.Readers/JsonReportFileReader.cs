using ExternalFiles.Readers.JsonModelReaders;
using MladostAir.Data;
using MladostAir.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;

namespace ExternalFiles.Readers
{
    public class JsonReportFileReader
    {
        private const string JsonDirectory = "../../../../ExternalFiles/JsonReports/dataJson.json";
        private const string CountriesJson = "../../../../ExternalFiles/JsonReports/countries.json";
        private const string CitiesJson = "../../../../ExternalFiles/JsonReports/cities.json";

        public static void ReadJsonFile()
        {
            var database = new MladostAirDbContext();

            string citiesJson = File.ReadAllText(CitiesJson);
            var cities = JsonConvert.DeserializeObject<List<City>>(citiesJson);

            foreach (var city in cities)
            {
                database.Cities.AddOrUpdate(city);
            }

            string countriesJson = File.ReadAllText(CountriesJson);
            var countries = JsonConvert.DeserializeObject<List<Country>>(countriesJson);

            foreach (var country in countries)
            {
                database.Countries.AddOrUpdate(country);
            }

            string jsonFile = File.ReadAllText(JsonDirectory);
            var tickets = JsonConvert.DeserializeObject<List<JsonTicket>>(jsonFile);

            foreach (var ticket in tickets)
            {
                var currTicket = new Ticket
                {
                    Destination = ticket.Destination,
                    Price = ticket.Price,
                    Airline = new Airline
                    {
                        Name = ticket.Airline
                    },
                    Airport = new Airport
                    {
                        Name = ticket.Airport.Name,
                        AirportCode = ticket.Airport.AirportCode
                    },
                    Customer = new Customer
                    {
                        FirstName = ticket.Customer.FirstName,
                        LastName = ticket.Customer.LastName,
                        Age = ticket.Customer.Age,
                        CustomerNumber = ticket.Customer.CustomerNumber
                    },
                };

                database.Tickets.Add(currTicket);
            }

            database.SaveChanges();
        }
    }
}
