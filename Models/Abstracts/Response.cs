using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime response_date { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        public string response { get; set; }

        #region CONSTRAINT
        /// <summary>
        /// Код пользователя, выставившего оценку
        /// </summary>
        [ForeignKey("user")]
        public int? user_id { get; set; }

        /// <summary>
        /// Пользователь, выставивший оценку
        /// </summary>
        public user user { get; set; }
        #endregion
    }
}
