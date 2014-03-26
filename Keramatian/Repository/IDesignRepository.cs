using Keramatian.Models;

namespace Keramatian.Repository
{
    public interface IDesignRepository : IRepository<Design>
    {
        Design GetDesignWithPriority(int priority);
    }
}
