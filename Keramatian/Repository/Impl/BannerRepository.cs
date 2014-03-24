using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
    {
        public BannerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}