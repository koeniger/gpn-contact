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

namespace WebApp.Services
{
    /// <summary>
    /// Сервис по сохранению картинки на диске
    /// </summary>
    public class ImageService
    {
        private readonly IWebHostEnvironment _env;
        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        /// <summary>
        /// Сохранение картинки на диске
        /// </summary>
        public async Task<string> SaveImage(IFormFile file, string table_name, int table_id)
        {
            var imageProfile = new ImageProfile();

            ValidateExtension(file, imageProfile);
            ValidateFileSize(file, imageProfile);

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
            return _env.WebRootFileProvider.GetFileInfo("images")?.PhysicalPath;
        }

        public IEnumerable<image> Indexation()
        {
            var imageProfile = new ImageProfile();

            var images = new List<image>();

            DirectoryInfo dir = new DirectoryInfo(GetWWWrootImages());
            foreach (var item in dir.GetDirectories())
            {
                FileInfo[] files = item.GetFiles();
                foreach (var file in files)
                {
                    try
                    {
                        ValidateExtension(file, imageProfile);

                        ValidateFileSize(file, imageProfile);

                    }
                    catch
                    { }
                }
            }

            return images;
        }
        /// <summary>
        /// Имя файла для сохранения на диске
        /// </summary>
        private string GetFileName(IFormFile file, int table_id)
        {
            return $"{Path.GetFileNameWithoutExtension(file.FileName.Replace(" ", ""))}_{table_id:D5}.{Path.GetExtension(file.FileName)}";
        }

        #region ValidateExtension
        /// <summary>
        /// Проверка на правильность расширения
        /// </summary>
        private void ValidateExtension(IFormFile file, ImageProfile imageProfile)
        {
            ValidateExtension(file.FileName, imageProfile);
        }

        /// <summary>
        /// Проверка на правильность расширения
        /// </summary>
        private void ValidateExtension(FileInfo file, ImageProfile imageProfile)
        {
            ValidateExtension(file.FullName, imageProfile);
        }

        private void ValidateExtension(string fileName, ImageProfile imageProfile)
        {
            var fileExtension = Path.GetExtension(fileName);

            if (imageProfile.AllowedExtensions.Any(ext => ext == fileExtension.ToLower()))
                return;

            throw new Exception("Extension error");
        }
        #endregion

        #region ValidateFileSize
        /// <summary>
        /// Проверка на превышение размера файлом
        /// </summary>
        private void ValidateFileSize(IFormFile file, ImageProfile imageProfile)
        {
            ValidateFileSize(file.Length, imageProfile);
        }

        /// <summary>
        /// Проверка на превышение размера файлом
        /// </summary>
        private void ValidateFileSize(FileInfo file, ImageProfile imageProfile)
        {
            ValidateFileSize(file.Length, imageProfile);
        }

        /// <summary>
        /// Проверка на превышение размера файлом
        /// </summary>
        private void ValidateFileSize(long fileLenght, ImageProfile imageProfile)
        {
            if (fileLenght > imageProfile.MaxSizeBytes)
                throw new Exception("Max size error");
        }
        #endregion
    }
}
