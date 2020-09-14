using Models.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.gpn
{
    /// <summary>
    /// Отзывы и предложения о продукте
    /// </summary>
    public class product_response : Response
    {
        /// <summary>
        /// Код отзыва и предложения о контрагенте
        /// </summary>
        [Key]
        public int product_response_id { get; set; }

        #region CONSTRAINT
        /// <summary>
        /// Код поставщика
        /// </summary>
        [ForeignKey("product")]
        public int? product_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        public product product { get; set; }

        /// <summary>
        /// Код родительского отзыва
        /// </summary>
        [ForeignKey("parent")]
        public int? parent_id { get; set; }

        /// <summary>
        /// Родительский отзыв
        /// </summary>
        public product_response parent { get; set; } = null!;
        #endregion

    }
}
