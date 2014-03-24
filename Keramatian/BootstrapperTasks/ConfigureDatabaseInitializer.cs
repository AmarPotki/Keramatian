using System.Data.Entity;
using Keramatian.Infrastructure.DataAccess;
using Keramatian.BootstrapperTasks;

namespace Keramatian.BootstrapperTasks
{
    public class ConfigureDatabaseInitializer : IBootstrapTask
    {
        public void Execute()
        {
#if (DEBUG)
            Database.SetInitializer(new DatabaseContextInitializer());
#endif
        }

        public int Priority
        {
            get { return 5; }
        }
    }
}