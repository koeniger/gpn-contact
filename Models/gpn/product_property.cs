using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.gpn
{
    /// <summary>
    /// Справочник свойств (атрибутов) материалов и оборудования
    /// <para>my_rel.products_properties</para>
    /// </summary>
    public class product_property
    {
        /// <summary>
        /// Код свойства продукции
        /// </summary>
        [Key]
        public int product_property_id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string property_name { get; set; }


        #region CONSTRAINT
        /// <summary>
        /// Код продукции
        /// </summary>
        [ForeignKey("product_type")]
        public int product_type_id { get; set; }

        public product_type product_type { get; set; }

        /// <summary>
        /// Код типа характеристики
        /// </summary>
        [ForeignKey("product_property_type")]
        public string product_property_type_id { get; set; }

        public product_property_type product_property_type { get; set; }

        /// <summary>
        /// Код единицы измерения
        /// </summary>
        [ForeignKey("okei")]
        public int okei_id { get; set; }

        public okei okei { get; set; }

        #endregion
    }
}
