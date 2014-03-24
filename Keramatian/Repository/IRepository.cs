using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Keramatian.Models;

namespace Keramatian.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T ById(int id);
        IEnumerable<T> All();
        IEnumerable<T> AllReadOnly();
        IEnumerable<T> AllByStoredProcedure(string spName, string dbSetName, SqlParameter[] parameters);
        int GetCount();
        // Add
        IEnumerable<T> GetAllAsNormal(Expression<Func<T, bool>> predicate);
        IEnumerable<T> All(string StrObject);
        EntityState GetEntityState(object entity);
    }
}
