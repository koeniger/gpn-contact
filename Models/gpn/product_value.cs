using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Models.gpn
{
    /// <summary>
    /// Справочник значений свойст продукции
    /// </summary>
    public class product_value
    {
        /// <summary>
        /// Код значения свойства
        /// </summary>
        [Key]
        public int product_value_id { get; set; }

        [Column("property_value")]
        public string value { get; set; }


        #region CONSTRAINT
        /// <summary>
        /// Код продукции
        /// </summary>
        [ForeignKey("product")]
        public int product_id { get; set; }

        public product product { get; set; }

        /// <summary>
        /// Код единицы измерения
        /// </summary>
        [ForeignKey("product_property")]
        public int product_property_id { get; set; }

        public product_property product_property { get; set; }

        #endregion
    }
}
