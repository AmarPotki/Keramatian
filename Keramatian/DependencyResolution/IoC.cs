using System.Web;
using Keramatian.BootstrapperTasks;
using Keramatian.Infrastructure.Logging;
using Keramatian.Repository;
using Keramatian.Repository.Impl;
using Keramatian.Services;
using Keramatian.Services.Impl;
using StructureMap;
using Keramatian.Infrastructure;


namespace Keramatian.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    //scan.AssemblyContainingType<IImageManager>();
                    scan.AddAllTypesOf<IBootstrapTask>();
                    scan.AddAllTypesOf<ILogger>();
                    scan.WithDefaultConventions();

                });
                // x.For<IUnitOfWork>().HttpContextScoped().Use<UnitOfWork>();
                // x.For<IDatabaseFactory>().HttpContextScoped().Use<DatabaseFactory>();


                x.For<HttpContext>().Use(() => HttpContext.Current);
                x.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));

                x.For<IUnitOfWork>().HttpContextScoped().Use<UnitOfWork>();
                x.For<IRoleRepository>().HttpContextScoped().Use<RoleRepository>();

                x.For<IAnnouncementRepository>().HttpContextScoped().Use<AnnouncementRepository>();
                x.For<IBackgroundColorRepository>().HttpContextScoped().Use<BackgroundColorRepository>();
                x.For<ICarpetRepository>().HttpContextScoped().Use<CarpetRepository>();
                x.For<IGradeRepository>().HttpContextScoped().Use<GradeRepository>();
                x.For<IPlainRepository>().HttpContextScoped().Use<PlainRepository>();
                x.For<ISizeRepository>().HttpContextScoped().Use<SizeRepository>();
                x.For<IPlainRepository>().HttpContextScoped().Use<PlainRepository>();
                x.For<IBannerRepository>().HttpContextScoped().Use<BannerRepository>();
                x.For<ICatalogRepository>().HttpContextScoped().Use<CatalogRepository>();
                x.For<ILeatherCarpetRepository>().HttpContextScoped().Use<LeatherCarpetRepository>();
                x.For<ITopBannerRepository>().HttpContextScoped().Use<TopBannerRepository>();
                x.For<ICarpetService>().HttpContextScoped().Use<CarpetService>();
            });

            return ObjectFactory.Container;
        }
    }
}