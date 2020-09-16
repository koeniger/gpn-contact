using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models.gpn
{
    /// <summary>
    /// Типы свойств (характеристик) прожукции
    /// </summary>
    public class product_property_type
    {
        /// <summary>
        /// Код типа свойства
        /// </summary>
        [Key]
        [NotNull]
        public Guid product_property_type_id { get; set; }

        /// <summary>
        /// Наименование типа
        /// </summary>
        [NotNull]
        public string property_type_name { get; set; }
    }
}
