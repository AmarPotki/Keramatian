using System.Data;
using System.Data.Entity.Infrastructure;
using Keramatian.Infrastructure.DataAccess;
using Keramatian.Infrastructure;

namespace Keramatian.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private DatabaseContext _database;
        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        protected DatabaseContext Database
        {
            get { return _database ?? (_database = _databaseFactory.Get()); }
        }

        public void Commit()
        {
            try
            {
                Database.SaveChanges();
                Database.Commit();
            }
            catch (DbUpdateException exp)
            {
                var inner = (UpdateException)exp.InnerException;

                foreach (var item in inner.StateEntries)
                {
                    //var tesst = item;
                }


                throw;
            }
        }
    }

}