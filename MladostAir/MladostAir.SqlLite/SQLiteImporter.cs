using MladostAir.SqlLite.Contracts;
using MladostAir.SqlLite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MladostAir.SqlLite
{
    public class SQLiteImporter
    {
        private readonly ISqliteDatabase database;
        private readonly StringBuilder builder;

        public SQLiteImporter(ISqliteDatabase database, StringBuilder builder)
        {
            this.database = database;
            this.builder = builder;
        }

        public string ImportData()
        {
            if (this.database.Hotels.FirstOrDefault() == null)
            {
                var hotelHilton = new Hotel { Name = "Hilton", CityId = 1 };
                var hotelMarinela = new Hotel { Name = "Marinela", CityId = 1 };
                var hotelGrand = new Hotel { Name = "Grand", CityId = 1 };
                var hotelMetropolitan = new Hotel { Name = "Metropolitan", CityId = 1 };

                var hotelMarica = new Hotel { Name = "Marica", CityId = 2 };
                var hotelNovotel = new Hotel { Name = "Novotel", CityId = 2 };

                var hotelsInSofia = new List<Hotel>();
                hotelsInSofia.Add(hotelHilton);
                hotelsInSofia.Add(hotelMarinela);
                hotelsInSofia.Add(hotelGrand);
                hotelsInSofia.Add(hotelMetropolitan);

                var hotelsInPlovdiv = new List<Hotel>();
                hotelsInPlovdiv.Add(hotelMarica);
                hotelsInPlovdiv.Add(hotelNovotel);

                var sofia = new City { Name = "Sofia", Hotels = hotelsInSofia };
                var plovdiv = new City { Name = "Plovdiv", Hotels = hotelsInPlovdiv };

                this.database.Hotels.AddRange(hotelsInSofia);
                this.database.Hotels.AddRange(hotelsInPlovdiv);

                this.database.Cities.Add(sofia);
                this.database.Cities.Add(plovdiv);

                this.database.SaveChanges();
            }

            return this.builder.ToString();
        }
    }
}
