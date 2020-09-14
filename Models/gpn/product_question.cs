
using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int product_question_id { get; set; }

        /// <summary>
        /// Вопрос/Ответ
        /// </summary>
        public string question { get; set; }

        /// <summary>
        /// Дата  высталение вопроса
        /// </summary>
        public DateTime questions_date { get; set; }

        #region CONSTRAINT
        /// <summary>
        /// Код продукта
        /// </summary>
        [ForeignKey("product")]
        public int? product_id { get; set; }
        /// <summary>
        /// Продукт
        /// </summary>
        public product product { get; set; }

        /// <summary>
        /// Код пользователя, оставившего вопрос
        /// </summary>
        [ForeignKey("user")]
        public int? user_id { get; set; }

        /// <summary>
        /// Пользователь, оставивший вопрос
        /// </summary>
        public user user { get; set; }

        /// <summary>
        /// Код родительского вопроса (для многоуровневых)
        /// </summary>
        [ForeignKey("parent")]
        public int? parent_id { get; set; }

        /// <summary>
        /// Родительский вопрос
        /// </summary>
        public product_question parent { get; set; } = null!;
        #endregion
    }
}
