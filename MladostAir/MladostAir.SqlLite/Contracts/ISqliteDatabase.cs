using MladostAir.SqlLite.Models;
using System.Data.Entity;

namespace MladostAir.SqlLite.Contracts
{
    public interface ISqliteDatabase
    {
        DbSet<Hotel> Hotels { get; set; }

        DbSet<City> Cities { get; set; }

        int SaveChanges();
    }
}
