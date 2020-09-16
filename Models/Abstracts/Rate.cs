using Models.secr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Models.Abstracts
{
    /// <summary>
    /// Оценка (рейтинг)
    /// </summary>
    public abstract class Rate
    {
        /// <summary>
        /// Оценка (рейтинг)
        /// </summary>
        [NotNull]
        public int rate { get; set; }

        /// <summary>
        /// Дата выставления оценки
        /// </summary>
        [NotNull]
        public DateTime rate_date { get; set; }

#region CONSTRAINT
        /// <summary>
        /// Код пользователя, выставившего оценку
        /// </summary>
        [ForeignKey("user")]
        [NotNull]
        public Guid user_id { get; set; }

        /// <summary>
        /// Пользователь, выставивший оценку
        /// </summary>
        [NotNull]
        public user user { get; set; }
#endregion
    }
}
