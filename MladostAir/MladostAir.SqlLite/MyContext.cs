using SQLite.CodeFirst;
using System.Data.Entity;

namespace MladostAir.SqlLite
{
    public class MyContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Build(Database.Connection);
            IDatabaseCreator sqliteDatabaseCreator = new SqliteDatabaseCreator();
            sqliteDatabaseCreator.Create(Database, model);
        }
    }
}
