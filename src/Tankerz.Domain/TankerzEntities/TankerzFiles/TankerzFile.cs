using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.TankerzFiles
{
    public class TankerzFile : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FolderPath { get; set; }
        public int FolderId { get; set; }
    }
}
