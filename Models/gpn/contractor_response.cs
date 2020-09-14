
using Models.Abstracts;
using Models.secr;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.gpn
{
    /// <summary>
    /// Отзывы и предложения о контрагенте
    /// </summary>
    public class contractor_response : Response
    {
        /// <summary>
        /// Код отзыва и предложения о контрагенте
        /// </summary>
        [Key]
        public int contractor_response_id { get; set; } 

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

        /// <summary>
        /// Код родительского отзыва
        /// </summary>
        [ForeignKey("parent")]
        public int? parent_id { get; set; }

        /// <summary>
        /// Родительский отзыв
        /// </summary>
        public contractor_response parent { get; set; } = null!;
        #endregion
    }
}
