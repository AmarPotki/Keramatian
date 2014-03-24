using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class PlainRepository : RepositoryBase<Plain>, IPlainRepository
    {
        public PlainRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}