using System.ComponentModel.DataAnnotations;


namespace ModelsStart.Dictionaries
{
    /// <summary>
    /// Справочник свойств (атрибутов) материалов и оборудования
    /// <para>my_rel.products_properties</para>
    /// </summary>
    public class ProductProperty
    {
        /// <summary>
        /// Код свойства продукции
        /// </summary>
        [Key]
        public int ProductPropertyId { get; set; }

        /// <summary>
        /// Код продукции
        /// </summary>
        public int ProductId { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Свойство (атрибут)
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Значение свойства
        /// </summary>
        public string Value { get; set; }

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
