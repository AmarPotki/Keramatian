using System;
using Keramatian.Infrastructure.DataAccess;

namespace Keramatian.Repository
{
    public interface IDatabaseFactory : IDisposable
    {
        DatabaseContext Get();
    }
}
