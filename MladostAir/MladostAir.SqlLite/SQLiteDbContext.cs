using MladostAir.SqlLite.Contracts;
using MladostAir.SqlLite.Models;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace MladostAir.SqlLite
{
    public class SQLiteDbContext : DbContext, ISqliteDatabase
    {
        public SQLiteDbContext()
            : base("Hotels")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLiteDbContext, Configuration>());
        }

        public virtual DbSet<Hotel> Hotels { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<SQLiteDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
