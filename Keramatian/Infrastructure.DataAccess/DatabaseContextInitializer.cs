using System.Data.Entity;

namespace Keramatian.Infrastructure.DataAccess
{
    public class DatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {

    }
}