using System.Web.Mvc;
using Keramatian.Filters;

namespace Keramatian
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogonAuthorize());

            var provider = new DebugFilterProvider();

            provider.Add(c => c.HttpContext.IsDebuggingEnabled ? new DebugMyAttribute() : null);

            FilterProviders.Providers.Add(provider);
        }
    }
}