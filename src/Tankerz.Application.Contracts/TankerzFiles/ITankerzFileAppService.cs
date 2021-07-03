using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tankerz.TankerzFiles
{
    public interface ITankerzFileAppService : IApplicationService
    {
        Task<bool> UploadFiles(List<UploadFileInput> data);
        Task<List<FilesOutput>> GetFilesInFolderAsync(int folderId = 0);
    }
}
