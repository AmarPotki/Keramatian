using Keramatian.Models;

namespace Keramatian.Repository
{
    public interface ITopBannerRepository : IRepository<TopBanner>
    {
        TopBanner GetOne();
    }
}
