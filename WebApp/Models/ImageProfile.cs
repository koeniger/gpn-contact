
using System.Collections.Generic;


namespace WebApp.Models
{
    public class ImageProfile
    {
        private const int mb = 1048576;

        public ImageProfile()
        {
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        }

        /// <summary>
        /// папка для сохранения 
        /// </summary>
        public string Folder => "gpn_contact";

        /// <summary>
        /// максимально разрешенный размер файла
        /// </summary>
        public int MaxSizeBytes => 2 * mb;

        /// <summary>
        /// Files Extensions
        /// </summary>
        public IEnumerable<string> AllowedExtensions { get; }
    }
}
