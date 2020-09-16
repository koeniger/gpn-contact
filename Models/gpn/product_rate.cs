using Models.Abstracts;
using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        [NotNull]
        public Guid product_rate_id { get; set; }

#region CONSTRAINT
        /// <summary>
        /// Код продукта
        /// </summary>
        [ForeignKey("product")]
        [NotNull]
        public Guid product_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        public product product { get; set; }
#endregion
    }
}
