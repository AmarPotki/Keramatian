using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class BackgroundColorRepository : RepositoryBase<BackgroundColor>, IBackgroundColorRepository
    {
        public BackgroundColorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}