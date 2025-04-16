using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Storage
{
    public class FileDiskStorageService : IStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _siteUrl;

        public FileDiskStorageService(IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;

            _siteUrl = configuration["AppSettings:SiteUrl"] ?? "";
        }

        public async Task<string> SaveFileAsync(IFormFile file, string directory)
        {
            if (file is null)
                return "";

            var fileInfo = new FileInfo(file.FileName);
            var extension = fileInfo.Extension;

            var fileNameWithExtension = $"{directory}\\{Guid.NewGuid().ToString()}{extension}";
            var path = $"{_webHostEnvironment.WebRootPath}\\{fileNameWithExtension}";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileNameWithExtension;
        }
        public async Task<string> ReplaceFileAsync(IFormFile file, string directory, string oldFile)
        {
            if (file is null)
                return oldFile;

            if (!string.IsNullOrEmpty(oldFile))
                DeleteFileIfExists(oldFile);

            return await SaveFileAsync(file, directory);
        }
        public void DeleteFileIfExists(string oldImage)
        {
            if (!string.IsNullOrEmpty(oldImage))
            {
                var oldPath = $"{_webHostEnvironment.WebRootPath}\\{oldImage}";
                File.Delete(oldPath);
            }
        }


        public string MapToUrl(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return "";

            if (imagePath.StartsWith('/'))
                imagePath = imagePath.Remove(0, 1);

            return $"{_siteUrl}/{imagePath.Replace(@"\", "/")}";
        }
    }
}
