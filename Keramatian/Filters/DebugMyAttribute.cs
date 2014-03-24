using System.Web;
using System.Web.Mvc;
using Keramatian.Controllers;
using Keramatian.Repository;
using Keramatian.Repository.Impl;

namespace Keramatian.Filters
{
    public class DebugMyAttribute : ActionFilterAttribute
    {
        private static IDatabaseFactory _databaseFactory;

        private readonly IRoleRepository _rolerepository;


        public DebugMyAttribute()
        {
            _databaseFactory = new DatabaseFactory();

            _rolerepository = new RoleRepository(_databaseFactory);

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller is AccountController)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

         
            filterContext.Controller.ViewBag.Mode = 1;

            if (HttpContext.Current.Session["RoleId"] != null)
            {
              //  url = new UrlHelper(filterContext.RequestContext);

               // var roleId = new Guid(HttpContext.Current.Session["RoleId"].ToString());

                // for menu item site



            }
        }
    }
}