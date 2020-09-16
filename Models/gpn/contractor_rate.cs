
using Models.Abstracts;
using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Models.gpn
{
    /// <summary>
    /// Оценки контрагента (рейтинг)
    /// </summary>
    public class contractor_rate : Rate
    {
        /// <summary>
        /// Код рейтинга контрагента
        /// </summary>
        [Key]
        [NotNull]
        public Guid contractor_rate_id { get; set; }

#region CONSTRAINT
        /// <summary>
        /// Код поставщика
        /// </summary>
        [ForeignKey("contractor")]
        [NotNull]
        public Guid contractor_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        [NotNull]
        public contractor contractor { get; set; }
#endregion
    }
}
