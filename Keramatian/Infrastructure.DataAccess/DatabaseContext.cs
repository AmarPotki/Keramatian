using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Keramatian.Models;

namespace Keramatian.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("Keramatian")
        {
            // Configuration.AutoDetectChangesEnabled = false;
            //Database.SetInitializer<DatabaseContext>(null);
            //Database.SetInitializer<DatabaseContext>(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
            // Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        public DbSet<BackgroundColor> BackgroundColors { get; set; }
        public DbSet<Carpet> Carpets { get; set; }
        public DbSet<LeatherCarpet> LeatherCarpets { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Plain> Plains { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<TopBanner> TopBanners { get; set; }



        public virtual void Commit()
        {

            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Size>()
            .HasMany(c => c.Carpets).WithMany(i => i.Sizes)
            .Map(t => t.MapLeftKey("SizeId")
                .MapRightKey("CarpetId")
                .ToTable("SizeCarpet"));

            modelBuilder.Entity<Size>()
            .HasMany(c => c.LeatherCarpets).WithMany(i => i.Sizes)
            .Map(t => t.MapLeftKey("SizeId")
                .MapRightKey("LeatherCarpetId")
                .ToTable("SizeLeatherCarpet"));
            //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // modelBuilder.Entity<Carpet>().HasOptional(d=>d.Grade);
        }



    }
}