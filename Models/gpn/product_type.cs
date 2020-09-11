using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.gpn
{
    /// <summary>
    /// Справочник типов продукции
    /// </summary>
    public class product_type
    {
        /// <summary>
        /// Код свойства Типа товара
        /// </summary>
        [Key]
        public int product_type_id { get; set; }

        /// <summary>
        /// Наимменование типа продукции
        /// </summary>
        public string product_type_name { get; set; }

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

        [JsonIgnore]
        public ICollection<product> Products { get; set; }
        [JsonIgnore]
        public ICollection<product_property> ProductProperties { get; set; }
    }
}
