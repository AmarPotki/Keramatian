using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public void Attach(Size size)
        {
            Database.Sizes.Attach(size);

        }
    }
}