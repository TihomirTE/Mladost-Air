using MladostAir.Data;
using MladostAir.Data.Migrations;
using MladostAir.Models;
using MladostAir.Models.Enum;
using System;
using System.Data.Entity;
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

            //var destination = new Destination
            //{
            //    Name = "Sofia",
            //};

            //db.Destinations.Add(destination);
            //db.SaveChanges();

            //Console.WriteLine(db.Destinations.Count());
        }
    }
}
