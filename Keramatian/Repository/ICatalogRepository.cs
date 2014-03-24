using Keramatian.Models;

namespace Keramatian.Repository
{
    public interface ICatalogRepository : IRepository<Catalog>
    {
        Catalog GetFirst();
    }
}
