using System.Web.Mvc;
using Keramatian.Infrastructure.Logging;

namespace Keramatian.BootstrapperTasks
{
    public class ConfigureAreas : IBootstrapTask
    {
        public void Execute()
        {
            AreaRegistration.RegisterAllAreas();
            LogUtility.Log.Info("Configuring Areas.");
        }

        public int Priority
        {
            get { return 10; }
        }
    }
}