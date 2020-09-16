
using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Models.gpn
{
    /// <summary>
    /// Вопросы и ответы
    /// </summary>
    public class product_question
    {
        /// <summary>
        /// Код вопроса
        /// </summary>
        [Key]
        [NotNull]
        public Guid product_question_id { get; set; }

        /// <summary>
        /// Вопрос/Ответ
        /// </summary>
        [NotNull]
        public string question { get; set; }

        /// <summary>
        /// Дата  высталение вопроса
        /// </summary>
        [NotNull]
        public DateTime questions_date { get; set; }

        #region CONSTRAINT
        /// <summary>
        /// Код продукта
        /// </summary>
        [ForeignKey("product")]
        [NotNull]
        public Guid product_id { get; set; }
        /// <summary>
        /// Продукт
        /// </summary>
        public product product { get; set; }

        /// <summary>
        /// Код пользователя, оставившего вопрос
        /// </summary>
        [ForeignKey("user")]
        [NotNull]
        public Guid user_id { get; set; }

        /// <summary>
        /// Пользователь, оставивший вопрос
        /// </summary>
        public user user { get; set; }

        /// <summary>
        /// Код родительского вопроса (для многоуровневых)
        /// </summary>
        [ForeignKey("parent")]
        public Guid? parent_id { get; set; }

        /// <summary>
        /// Родительский вопрос
        /// </summary>
        public product_question parent { get; set; } = null!;
        #endregion
    }
}
