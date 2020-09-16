
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models.gpn
{
    public class image
    {
        /// <summary>
        /// Код свойства Типа товара
        /// </summary>
        [Key]
        [NotNull]
        public Guid image_id { get; set; }

        /// <summary>
        /// Код из любой таблицы
        /// </summary>
        public Guid any_table_id { get; set; }

        /// <summary>
        /// Нименование таблицы
        /// </summary>
        public string any_table_name { get; set; }

        /// <summary>
        /// Путь до изображения в файловой системе
        /// </summary>
        public string image_path { get; set; }

        /// <summary>
        /// Флаг основного изображения (для галереи изображения)
        /// </summary>
        [NotNull]
        public bool is_main { get; set; }
    }
}
