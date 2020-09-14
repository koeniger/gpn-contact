using System.ComponentModel.DataAnnotations;


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
        public string product_property_type_id { get; set; }

        /// <summary>
        /// Наименование типа
        /// </summary>
        public string property_type_name { get; set; }
    }
}
