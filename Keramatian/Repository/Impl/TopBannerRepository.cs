using System.Linq;
using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class TopBannerRepository : RepositoryBase<TopBanner>, ITopBannerRepository
    {
        public TopBannerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public TopBanner GetOne()
        {
            return !Database.TopBanners.Any() ? new TopBanner { Id = 0, ImageName = "", ImagePath = "/Images/StaticBanner.png" } : Database.TopBanners.OrderByDescending(x=>x.Id).First();
        }
    }
}