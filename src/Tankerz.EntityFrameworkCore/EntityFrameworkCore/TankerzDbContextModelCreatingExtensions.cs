using Microsoft.EntityFrameworkCore;
using Volo.Abp;

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
        }
    }
}