using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tankerz.Helper;
using Tankerz.TankerzEntities.TankerzFiles;
using Tankerz.TankerzEntities.TankerzFolders;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tankerz.TankerzFiles
{
    public class TankerzFileAppService : ApplicationService, ITankerzFileAppService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<TankerzFile, Guid> _tankerzFileRepository;
        private readonly IRepository<TankerzFolder, int> _tankerzFolderRepository;
        public TankerzFileAppService(
            IWebHostEnvironment webHostEnvironment,
            IRepository<TankerzFile, Guid> tankerzFileRepository,
            IRepository<TankerzFolder, int> tankerzFolderRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _tankerzFileRepository = tankerzFileRepository;
            _tankerzFolderRepository = tankerzFolderRepository;
        }
        public async Task<List<FilesOutput>> GetFilesInFolderAsync(int folderId)
        {
            if (System.IO.Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "Media")))
            {
                var folder = await _tankerzFolderRepository.FirstOrDefaultAsync(x => x.Id.Equals(folderId));
                if (folder != null)
                {
                    var data = await _tankerzFileRepository.GetListAsync(x => x.FolderId.Equals(folderId));
                    // get FolderPath
                    var folderPath = Path.Combine("/Media", folder.Name).Replace("\\", "/");
                    // end get FolderPath
                    return data.Select(x => new FilesOutput
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Extension = Path.GetExtension(x.FilePath).ToLower(),
                        ImageSmallUrl = Path.Combine(folderPath, "small", x.FilePath).Replace("\\", "/"),
                        Url = Path.Combine(folderPath, x.FilePath).Replace("\\", "/"),
                        Checked = false
                    }).ToList();
                }
            }
            return new List<FilesOutput>();
        }

        public async Task<bool> UploadFiles(List<UploadFileInput> data)
        {
            if (!System.IO.Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "Media")))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "Media"));
            }
            try
            {
                foreach (var item in data)
                {
                    var fileName = StringHelper.ConvertUploadFileName(item.Name);
                    // check image duplicate name
                    var folder = await _tankerzFolderRepository.FirstOrDefaultAsync(x => x.Id.Equals(item.FolderId));
                    if (folder != null)
                    {
                        var fileExtension = item.Extension;
                        var byteArray = Convert.FromBase64String(item.Base64String);
                        // convert image to Webp
                        using (var stream = new MemoryStream(byteArray))
                        {
                            string[] allowedImageTypes = new string[] { "image/jpeg", "image/png" };
                            var file = new FormFile(stream, 0, byteArray.Length, fileName, fileName + fileExtension)
                            {
                                Headers = new HeaderDictionary(),
                                ContentType = item.ContentType,
                            };

                            var folderPath = Path.Combine("Media", folder.Name).Replace("\\", "/");
                            //
                            if (allowedImageTypes.Contains(file.ContentType.ToLower()))
                            {
                                fileExtension = ".webp"; // assign file extension
                            }
                            var isExistsFileName = System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath, fileName + fileExtension));
                            if (isExistsFileName)
                            {
                                fileName = getNextFileName(fileName + fileExtension, Path.Combine(_webHostEnvironment.WebRootPath, folderPath));
                            }

                            //
                            if (allowedImageTypes.Contains(file.ContentType.ToLower()))
                            {
                                // Then save in WebP format
                                try
                                {
                                    var webpPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, fileName + fileExtension);
                                    using (FileStream webPFileStream = new FileStream(webpPath, FileMode.Create))
                                    {
                                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                                        {
                                            imageFactory.Load(file.OpenReadStream())
                                                        .Format(new WebPFormat())
                                                        .Quality(50)
                                                        .Save(webPFileStream);
                                        }
                                    }
                                    var webpSmallPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, "small", fileName + fileExtension);
                                    using (FileStream webPFileStream = new FileStream(webpSmallPath, FileMode.Create))
                                    {
                                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                                        {
                                            imageFactory.Load(file.OpenReadStream())
                                                        .Format(new WebPFormat())
                                                        .Quality(50)
                                                        .Resize(new ImageProcessor.Imaging.ResizeLayer(new Size(200, 200), ImageProcessor.Imaging.ResizeMode.Crop, ImageProcessor.Imaging.AnchorPosition.Center))
                                                        .Save(webPFileStream);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                            else
                            {
                                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, fileName + fileExtension);
                                System.IO.File.WriteAllBytes(filePath, byteArray);

                                // write small image
                                string fileSmallPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, "small", fileName + fileExtension);
                                using (FileStream webPFileStream = new FileStream(fileSmallPath, FileMode.Create))
                                {
                                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                                    {
                                        imageFactory.Load(file.OpenReadStream())
                                                    .Resize(new ImageProcessor.Imaging.ResizeLayer(new Size(200, 200), ImageProcessor.Imaging.ResizeMode.Crop, ImageProcessor.Imaging.AnchorPosition.Center))
                                                    .Save(webPFileStream);
                                    }
                                }
                            }
                        }

                        // save to database
                        var model = new TankerzFile
                        {
                            Name = item.Name,
                            FilePath = fileName + fileExtension,
                            FolderPath = folder.Name,
                            FolderId = folder.Id
                        };
                        await _tankerzFileRepository.InsertAsync(model);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private string getNextFileName(string fileName, string folderPath)
        {
            string extension = Path.GetExtension(fileName);

            int i = 0;
            while (System.IO.File.Exists(Path.Combine(folderPath, fileName)))
            {
                if (i == 0)
                    fileName = fileName.Replace(extension, "(" + ++i + ")" + extension);
                else
                    fileName = fileName.Replace("(" + i + ")" + extension, "(" + ++i + ")" + extension);
            }

            return Path.GetFileNameWithoutExtension(fileName);
        }
    }
}
