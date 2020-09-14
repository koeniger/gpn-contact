
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.gpn
{
    /// <summary>
    /// Разделы справочника материалов и оборудования
    /// </summary>
    public class product_directory
    {
        /// <summary>
        /// Код родительского раздела
        /// </summary>
        [Key]
        public int product_directory_id { get; set; }

        /// <summary>
        /// Нименование раздела
        /// </summary>
        public string product_directory_name { get; set; }

        /// <summary>
        /// Описание раздела
        /// </summary>
        public string description { get; set; }

        #region CONSTRAINT
        /// <summary>
        /// Код родительского раздела
        /// </summary>
        [ForeignKey("parent")]
        public int? parent_id { get; set; }

        /// <summary>
        /// Родительского раздел
        /// </summary>
        public product_directory parent { get; set; } = null!;

        #endregion 

        /// <summary>
        /// Продукты, которые входят в каталог
        /// </summary>
        [JsonIgnore]
        public ICollection<product> Products { get; set; }

        /// <summary>
        /// Потомки (Подразделы)
        /// </summary>
        [JsonIgnore]
        public ICollection<product_directory> Subdirectories { get; set; }
    }
}
