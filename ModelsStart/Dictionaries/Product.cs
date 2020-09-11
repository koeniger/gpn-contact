using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelsStart.Dictionaries
{
    /// <summary>
    /// Справочник материалов и оборудования
    /// <para>my_rel.products</para>
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Код продукции
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Наимменование продукции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание краткое
        /// </summary>
        public string DescriptionShort { get; set; }

        /// <summary>
        /// Описание полное
        /// </summary>
        public string DescriptionFull { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public int ContractorId { get; set; }

        /// <summary>
        /// Код типа продукта
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Тип продукции
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Справочник свойств (атрибутов) продукции
        /// </summary>
        [JsonIgnore]
        public ICollection<ProductProperty> ProductProperties { get; set; }

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
    }
}
