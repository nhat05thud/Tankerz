using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tankerz.Data;
using Volo.Abp.DependencyInjection;

namespace Tankerz.EntityFrameworkCore
{
    public class EntityFrameworkCoreTankerzDbSchemaMigrator
        : ITankerzDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreTankerzDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the TankerzMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<TankerzMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}