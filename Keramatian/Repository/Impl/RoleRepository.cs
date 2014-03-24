using Keramatian.Models;


namespace Keramatian.Repository.Impl
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}