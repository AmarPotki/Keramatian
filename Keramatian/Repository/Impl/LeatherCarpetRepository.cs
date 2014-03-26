using System.Collections.Generic;
using System.Linq;
using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class LeatherCarpetRepository : RepositoryBase<LeatherCarpet>, ILeatherCarpetRepository
    {
        public LeatherCarpetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IEnumerable<LeatherCarpet> GetLeatherCarpets(int? page, int pageSize)
        {
            if (!Database.LeatherCarpets.Any())
            {
                return new List<LeatherCarpet>();
            }
            var pageIndex = (page ?? 1) - 1;
            return Database.LeatherCarpets.OrderBy(x => x.Priority).Skip(pageSize * pageIndex).Take(pageSize);
        }

        public LeatherCarpet GetCarpetWithSize(int leatherCarpetId)
        {
            return Database.LeatherCarpets.Include("Sizes").First(x => x.Id == leatherCarpetId);
        }
        public override IEnumerable<LeatherCarpet> All()
        {
            return Database.LeatherCarpets.Include("Design");
        }
    }
}