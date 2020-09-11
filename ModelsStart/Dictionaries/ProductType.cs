using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsStart.Dictionaries
{
    /// <summary>
    /// Справочник типов продукции
    /// </summary>
    public class ProductType
    {
        /// <summary>
        /// Код свойства Типа товара
        /// </summary>
        [Key]
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Наимменование типа продукции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Статус продукции: Действующий/Архивный
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime DateChange { get; set; }

        /// <summary>
        /// Дата отправки записи в архив
        /// </summary>
        public DateTime DateArchive { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public ICollection<ProductTypeProperty> ProductTypeProperties { get; set; }
    }
}
