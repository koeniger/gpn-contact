
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApp.Models
{
    public class ImageProfile
    {
        public int MaxSizeBytes { get; set; }

        /// <summary>
        /// Files Extensions
        /// </summary>
        public List<string> AllowedExtensions { get; set; }


        public string Path { get; set; }

        public void Validate(IFormFile file)
        {
            ValidateExtension(file.FileName);
            ValidateFileSize(file.Length);
        }

        public void Validate(FileInfo file)
        {
            ValidateExtension(file.FullName);
            ValidateFileSize(file.Length);
        }

        #region ValidateExtension

        private void ValidateExtension(string fileName)
        {
            var fileExtension = System.IO.Path.GetExtension(fileName);

            if (AllowedExtensions.Any(ext => ext == fileExtension.ToLower()))
                return;

            throw new Exception("Extension error");
        }
        #endregion

        #region ValidateFileSize

        /// <summary>
        /// Проверка на превышение размера файлом
        /// </summary>
        private void ValidateFileSize(long fileLenght)
        {
            if (fileLenght > MaxSizeBytes)
                throw new Exception("Max size error");
        }
        #endregion
    }
}
