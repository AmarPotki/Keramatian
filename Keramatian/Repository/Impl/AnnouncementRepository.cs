using System.Collections.Generic;
using Keramatian.Models;
using System.Linq;

namespace Keramatian.Repository.Impl
{
    public class AnnouncementRepository : RepositoryBase<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public IEnumerable<Announcement> GetLastsAnnouncements()
        {
            return Database.Announcements.Take(6);
        }
    }
}