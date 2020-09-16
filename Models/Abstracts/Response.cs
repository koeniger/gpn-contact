using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Models.Abstracts
{
    /// <summary>
    /// Отзывы
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// Дата написания отзыва
        /// </summary>
        [NotNull]
        public DateTime response_date { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        [NotNull]
        public string response { get; set; }

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
