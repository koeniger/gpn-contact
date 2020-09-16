using Microsoft.AspNetCore.Http;
using System.Linq;
using WebApp.Models;
using System.Drawing;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Models.gpn;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace WebApp.Services
{
    /// <summary>
    /// Сервис по сохранению картинки на диске
    /// </summary>
    public class ImageService
    {
        private readonly ImageProfile imageProfile;

        public ImageService(IOptions<ImageProfile> opthion)
        {
            imageProfile = opthion.Value;
        }
        /// <summary>
        /// Сохранение картинки на диске
        /// </summary>
        public async Task<string> SaveImage(IFormFile file, string table_name, Guid table_id)
        {
            imageProfile.Validate(file);

            var fileName = GetFileName(file, table_id);

            var path = $"{GetWWWrootImages()}\\{table_name}\\{fileName}";

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var img = Image.FromStream(file.OpenReadStream()))
            {
                await Task.Run(() => img.Save(path, img.RawFormat));
            }

            return path;
        }

        private string GetWWWrootImages()
        {
            return imageProfile.Path;
        }

        /// <summary>
        /// Имя файла для сохранения на диске
        /// </summary>
        private string GetFileName(IFormFile file, Guid table_id)
        {
            return $"{Path.GetFileNameWithoutExtension(file.FileName.Replace(" ", ""))}_{table_id}.{Path.GetExtension(file.FileName)}";
        }
    }
}
