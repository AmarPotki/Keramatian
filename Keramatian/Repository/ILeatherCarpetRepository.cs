using System.Collections.Generic;
using Keramatian.Models;

namespace Keramatian.Repository
{
    public interface ILeatherCarpetRepository : IRepository<LeatherCarpet>
    {
        IEnumerable<LeatherCarpet> GetLeatherCarpets(int? page, int pageSize);
        LeatherCarpet GetCarpetWithSize(int leatherCarpetId);
    }
}
