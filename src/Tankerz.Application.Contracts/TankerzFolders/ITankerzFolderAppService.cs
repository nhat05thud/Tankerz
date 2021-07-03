using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tankerz.TankerzFolders
{
    public interface ITankerzFolderAppService : IApplicationService
    {
        Task<bool> CreateFolder(string name);
        Task<List<FoldersOutput>> GetFolders();
    }
}
