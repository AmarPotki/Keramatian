using System.Linq;
using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class DesignRepository : RepositoryBase<Design>, IDesignRepository
    {
        public DesignRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public Design GetDesignWithPriority(int priority)
        {
            return Database.Designs.FirstOrDefault(pr => pr.Priority == priority && pr.IsCarpet);
        }
    }
}