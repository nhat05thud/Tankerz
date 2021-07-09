using Microsoft.EntityFrameworkCore;
using Tankerz.TankerzEntities.BlogCategories;
using Tankerz.TankerzEntities.Blogs;
using Tankerz.TankerzEntities.ProductAttributeOptions;
using Tankerz.TankerzEntities.ProductAttributes;
using Tankerz.TankerzEntities.ProductCategories;
using Tankerz.TankerzEntities.ProductGroups;
using Tankerz.TankerzEntities.Products;
using Tankerz.TankerzEntities.ProductWithMultipleAttributeOptions;
using Tankerz.TankerzEntities.TankerzFiles;
using Tankerz.TankerzEntities.TankerzFolders;
using Tankerz.TankerzEntities.TankerzPages;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tankerz.EntityFrameworkCore
{
    public static class TankerzDbContextModelCreatingExtensions
    {
        public static void ConfigureTankerz(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(TankerzConsts.DbTablePrefix + "YourEntities", TankerzConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<TankerzFile>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "TankerzFiles",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });
            builder.Entity<TankerzFolder>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "TankerzFolders",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });
            builder.Entity<TankerzPage>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "TankerzPages",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });


            builder.Entity<Blog>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "Blogs",
                          TankerzConsts.DbSchema);

                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });
            builder.Entity<BlogCategory>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "BlogCategories",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });


            builder.Entity<Product>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "Products",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
                //b.Property(x => x.Price).HasColumnType("decimal(18,2)");
                //b.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");
            });
            builder.Entity<ProductCategory>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "ProductCategories",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });
            builder.Entity<ProductGroup>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "ProductGroups",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });

            builder.Entity<ProductAttribute>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "ProductAttributes",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });
            builder.Entity<ProductAttributeOption>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "ProductAttributeOptions",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            });

            builder.Entity<ProductWithMultipleAttributeOption>(b =>
            {
                b.ToTable(TankerzConsts.DbTablePrefix + "ProductWithMultipleAttributeOptions",
                          TankerzConsts.DbSchema);
                b.ConfigureByConvention();
            });
        }
    }
}