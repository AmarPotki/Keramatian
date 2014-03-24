using System.Web.Mvc;
using StructureMap;
using System.Linq;
using Keramatian.DependencyResolution;

namespace Keramatian.BootstrapperTasks
{
    public static class Bootstrapper
    {
        static Bootstrapper()
        {
            var container = (IContainer)IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }

        public static void Initialize()
        {
            var items = ObjectFactory.GetAllInstances<IBootstrapTask>();
            foreach (var bootstrapTask in items.OrderByDescending(x => x.Priority))
            {
                bootstrapTask.Execute();
            }
        }
    }

}