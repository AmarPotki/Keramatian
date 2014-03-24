using System.Linq;
using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
    {
        public CatalogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public Catalog GetFirst()
        {
            return Database.Catalogs.Any() ? Database.Catalogs.First() : null;
        }
    }
}