using Keramatian.Models;
using System.Collections.Generic;

namespace Keramatian.Repository
{
    public interface IAnnouncementRepository : IRepository<Announcement>
    {
        IEnumerable<Announcement> GetLastsAnnouncements();
    }
}
