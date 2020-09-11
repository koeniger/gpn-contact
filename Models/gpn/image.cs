
using System.ComponentModel.DataAnnotations;

namespace Models.gpn
{
    public class image
    {
        /// <summary>
        /// Код свойства Типа товара
        /// </summary>
        [Key]
        public int image_id { get; set; }

        /// <summary>
        /// Код из любой таблицы
        /// </summary>
        public int any_table_id { get; set; }

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
        public bool is_main { get; set; }
    }
}
