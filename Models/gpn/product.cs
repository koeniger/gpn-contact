using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.gpn
{
    /// <summary>
    /// Справочник материалов и оборудования
    /// <para>my_rel.products</para>
    /// </summary>
    public class product
    {
        /// <summary>
        /// Код продукции
        /// </summary>
        [Key]
        public int product_id { get; set; }

        /// <summary>
        /// Наимменование продукции
        /// </summary>
        public string product_name { get; set; }

        /// <summary>
        /// Описание краткое
        /// </summary>
        public string description_short { get; set; }

        /// <summary>
        /// Описание полное
        /// </summary>
        public string description_full { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// Справочник свойств (атрибутов) продукции
        /// </summary>
        [JsonIgnore]
        public ICollection<product_value> products_values { get; set; }

        /// <summary>
        /// Статус продукции: Действующий/Архивный
        /// </summary>
        public bool is_archive { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime date_change { get; set; }

        /// <summary>
        /// Дата отправки записи в архив
        /// </summary>
        public DateTime date_archive { get; set; }

        #region CONSTRAINT
        /// <summary>
        /// Код раздела справочника продукции
        /// </summary>
        [ForeignKey("product_directory")]
        public int? product_directory_id { get; set; }
        /// <summary>
        /// Раздел
        /// </summary>
        public product_directory product_directory { get; set; }

        /// <summary>
        /// Код поставщика
        /// </summary>
        [ForeignKey("contractor")]
        public int? contractor_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        public contractor contractor { get; set; }

        /// <summary>
        /// Код типа продукта
        /// </summary>
        [ForeignKey("product_type")]
        public int? product_type_id { get; set; }

        /// <summary>
        /// Тип продукции
        /// </summary>
        public product_type product_type { get; set; }
        #endregion
    }
}
