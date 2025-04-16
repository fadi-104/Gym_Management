using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Storage
{
    public interface IStorageService
    {
        void DeleteFileIfExists(string oldImage);
        string MapToUrl(string imagePath);
        Task<string> ReplaceFileAsync(IFormFile file, string directory, string oldFile);
        Task<string> SaveFileAsync(IFormFile file, string directory);
    }
}
