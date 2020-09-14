
using Models.Abstracts;
using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int contractor_rate_id { get; set; }

#region CONSTRAINT
        /// <summary>
        /// Код поставщика
        /// </summary>
        [ForeignKey("contractor")]
        public int? contractor_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        public contractor contractor { get; set; }
#endregion
    }
}
