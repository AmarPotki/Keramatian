using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class GradeRepository : RepositoryBase<Grade>, IGradeRepository
    {
        public GradeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}