using Models.Abstracts;
using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.gpn
{
    /// <summary>
    /// Оценка продукта (рейтинг)
    /// </summary>
    public class product_rate : Rate
    {
        /// <summary>
        /// Код рейтинга
        /// </summary>
        [Key]
        public int product_rate_id { get; set; }

#region CONSTRAINT
        /// <summary>
        /// Код продукта
        /// </summary>
        [ForeignKey("product")]
        public int? product_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        public product product { get; set; }
#endregion
    }
}
