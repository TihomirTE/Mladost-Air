using ExternalFiles.Readers.JsonModelReaders;
using MladostAir.Data;
using System.Data.Entity;
using MladostAir.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MladostAir.Models.Enum;

namespace ExternalFiles.Readers
{
    public class JsonReportFileReader
    {
        private const string JsonDirectory = "../../../../ExternalFiles/JsonReports/dataJson.json";

        public static void ReadJsonFile()
        {
            string jsonFile = File.ReadAllText(JsonDirectory);

            var jsonModels = JsonConvert.DeserializeObject<JsonTicket>(jsonFile);
            var customerModel = jsonModels.Customer;
            var destination = jsonModels.Destination;
            var price = jsonModels.Price;
            var airportModel = jsonModels.Airport;
            var cityModel = jsonModels.City;
            var countryModel = jsonModels.Country;
            var airlineModel = jsonModels.Airline;

            var database = new MladostAirDbContext();

            var airline = new Airline();
            var airport = new Airport();
            var city = new City();
            var country = new Country();
            var customer = new Customer();
            var ticket = new Ticket();

            // import customer
            customer.FirstName = customerModel.FirstName;
            customer.LastName = customerModel.LastName;
            customer.CustomerNumber = customerModel.CustomerNumber;
            customer.Age = customerModel.Age;
            database.Customers.Add(customer);

            //// import airport
            airport.Name = airportModel.Name;
            airport.AirportCode = airportModel.AirportCode;
            database.Airports.Add(airport);

            //// import airiline
            airline.Name = airlineModel;

            //// import ticket
            ticket.Destination = destination;
            ticket.Price = price;
            database.Tickets.Add(ticket);

            //// import city
            city.Name = cityModel;
            database.Cities.Add(city);

            //// import country
            country.Name = countryModel;
            database.Countries.Add(country);
            database.SaveChanges();
        }
    }
}
