using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankerz.Helper;
using Tankerz.TankerzEntities.TankerzFolders;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.TankerzFolders
{
    public class TankerzFolderAppService : ApplicationService, ITankerzFolderAppService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<TankerzFolder, int> _tankerzFolderRepository;
        public TankerzFolderAppService(IWebHostEnvironment webHostEnvironment, IRepository<TankerzFolder, int> tankerzFolderRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _tankerzFolderRepository = tankerzFolderRepository;
        }
        public async Task<bool> CreateFolder(string name)
        {
            if (!System.IO.Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "Media")))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "Media"));
            }
            try
            {
                var folderName = StringHelper.ConvertUploadFileName(name);
                var isExistFolderName = await _tankerzFolderRepository.FirstOrDefaultAsync(x => x.Name.Equals(folderName));
                if (isExistFolderName != null)
                {
                    return false;
                }
                var model = new TankerzFolder
                {
                    Name = folderName
                };
                await _tankerzFolderRepository.InsertAsync(model);
                // Create folder in local
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Media", folderName);
                var folderSmallPath = Path.Combine(_webHostEnvironment.WebRootPath, "Media", folderName, "small");
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                if (!System.IO.Directory.Exists(folderSmallPath))
                {
                    System.IO.Directory.CreateDirectory(folderSmallPath);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<FoldersOutput>> GetFolders()
        {
            var data = await _tankerzFolderRepository.GetListAsync();
            return data.Select(x => new FoldersOutput { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}
