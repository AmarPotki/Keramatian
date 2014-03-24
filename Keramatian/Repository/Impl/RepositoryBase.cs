using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using Keramatian.Infrastructure.DataAccess;
using Keramatian.Common;
using Keramatian.Models;

namespace Keramatian.Repository.Impl
{
    public abstract class RepositoryBase<T> where T : class, IEntity
    {
        private readonly DatabaseContext _database;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly IDbSet<T> _dbset;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            Check.Argument.IsNotNull(databaseFactory, "databaseFactory");
            _databaseFactory = databaseFactory;

            _database = _database ?? (_database = _databaseFactory.Get());
            _dbset = Database.Set<T>();

        }

        protected DatabaseContext Database
        {
            get { return _database; }
        }

        public virtual void Add(T entity)
        {
            _database.Entry(entity).State = EntityState.Added;
            _dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _database.Entry(entity).State = EntityState.Modified;
        }

        public virtual T ById(int id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> All()
        {
            return _dbset.ToList();
        }

        public virtual IEnumerable<T> AllReadOnly()
        {
            return _dbset.AsNoTracking().ToList();

        }
        /// <summary>
        /// Get Total Item
        /// </summary>
        /// <returns></returns>
        public virtual int GetCount()
        {
            return _dbset.Count();
        }

        public virtual IEnumerable<T> AllByStoredProcedure(string spName, string dbSetName, SqlParameter[] parameters)
        {
            DbConnection conn = _database.Database.Connection;
            ObjectContext objectContext = ((IObjectContextAdapter)_database).ObjectContext;

            conn.Open();

            DbCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = spName;

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            DbDataReader reader = command.ExecuteReader();
            List<T> collection = objectContext.Translate<T>(reader, dbSetName, MergeOption.AppendOnly).ToList();
            conn.Close();

            return collection;
        }

        //add
        public IEnumerable<T> GetAllAsNormal(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).OrderByDescending(e => e.Id).ToList();
        }

        public virtual IEnumerable<T> All(string strObject)
        {
            return _dbset.Include(strObject).ToList();
        }

        public EntityState GetEntityState(object entity)
        {
            var ctx = ((IObjectContextAdapter)_database).ObjectContext;
            ctx.DetectChanges();

            return ctx.ObjectStateManager.GetObjectStateEntry(entity).State;
        }
    }

}