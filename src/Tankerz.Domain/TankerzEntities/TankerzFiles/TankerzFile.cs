using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tankerz.TankerzEntities.TankerzFiles
{
    public class TankerzFile : FullAuditedAggregateRoot<int>
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FolderPath { get; set; }
        public int FolderId { get; set; }
    }
}
