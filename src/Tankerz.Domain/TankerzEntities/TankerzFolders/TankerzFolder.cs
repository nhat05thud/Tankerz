using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.TankerzFolders
{
    public class TankerzFolder : FullAuditedAggregateRoot<int>
    {
        public string Name { get; set; }
    }
}
