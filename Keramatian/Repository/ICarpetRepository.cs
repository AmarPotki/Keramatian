using System.Collections.Generic;
using Keramatian.Models;

namespace Keramatian.Repository
{
    public interface ICarpetRepository : IRepository<Carpet>
    {
        IEnumerable<Carpet> GetCarpets(int priority);
        IEnumerable<Carpet> GetCarpets(int? page, int pageSize);
        Carpet GetCarpetWithSize(int carpetId);
        int GetDesignCount();

    }
}
