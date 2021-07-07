using Microsoft.EntityFrameworkCore;
using Tankerz.TankerzEntities.BlogCategories;
using Tankerz.TankerzEntities.Blogs;
using Tankerz.TankerzEntities.ProductAttributeOptions;
using Tankerz.TankerzEntities.ProductAttributes;
using Tankerz.TankerzEntities.ProductCategories;
using Tankerz.TankerzEntities.ProductGroups;
using Tankerz.TankerzEntities.Products;
using Tankerz.TankerzEntities.TankerzFiles;
using Tankerz.TankerzEntities.TankerzFolders;
using Tankerz.TankerzEntities.TankerzPages;
using Tankerz.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Tankerz.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See TankerzMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class TankerzDbContext : AbpDbContext<TankerzDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<TankerzFile> TankerzFiles { get; set; }
        public DbSet<TankerzFolder> TankerzFolders { get; set; }
        public DbSet<TankerzPage> TankerzPages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeOption> ProductAttributeOptions { get; set; }



        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside TankerzDbContextModelCreatingExtensions.ConfigureTankerz
         */

        public TankerzDbContext(DbContextOptions<TankerzDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the TankerzEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureTankerz method */

            builder.ConfigureTankerz();
        }
    }
}
