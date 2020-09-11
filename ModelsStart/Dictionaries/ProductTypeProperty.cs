using System.ComponentModel.DataAnnotations;

namespace ModelsStart.Dictionaries
{
    /// <summary>
    /// Справочник свойств (атрибутов) типов продукции
    /// <para> my_rel.products_types_properties</para>
    /// </summary>
    public class ProductTypeProperty
    {
        /// <summary>
        /// Код свойства Типа товара
        /// </summary>
        [Key]
        public int ProductTypePropertyId { get; set; }

        /// <summary>
        /// Код типа товара
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Свойство (атрибут)
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Значение свойства
        /// </summary>
        public string Value { get; set;}
        
        /// <summary>
        /// Строка/целое/дробь
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// Код единицы измерения
        /// </summary>
        public int ValueOkeiId { get; set; }

        public OkEI OkEI { get; set; }
    }
}
