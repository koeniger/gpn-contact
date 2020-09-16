using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        [NotNull]
        public Guid product_id { get; set; }

        /// <summary>
        /// Наимменование продукции
        /// </summary>
        [NotNull]
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
        [NotNull]
        public decimal price { get; set; }

        /// <summary>
        /// Справочник свойств (атрибутов) продукции
        /// </summary>
        [JsonIgnore]
        public ICollection<product_value> products_values { get; set; }

        /// <summary>
        /// Статус продукции: Действующий/Архивный
        /// </summary>
        [NotNull]
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
        public Guid product_directory_id { get; set; }
        /// <summary>
        /// Раздел
        /// </summary>
        public product_directory product_directory { get; set; }

        /// <summary>
        /// Код поставщика
        /// </summary>
        [ForeignKey("contractor")]
        public Guid contractor_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        public contractor contractor { get; set; }

        /// <summary>
        /// Код типа продукта
        /// </summary>
        [ForeignKey("product_type")]
        public Guid product_type_id { get; set; }

        /// <summary>
        /// Тип продукции
        /// </summary>
        public product_type product_type { get; set; }
        #endregion

        [JsonIgnore]
        public ICollection<product_response> Responses { get; set; }

        [JsonIgnore]
        public ICollection<product_rate> Rates { get; set; }

        [JsonIgnore]
        public ICollection<product_question> Questions { get; set; }
    }
}
