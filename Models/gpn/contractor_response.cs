
using Models.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        [NotNull]
        public Guid contractor_response_id { get; set; } 

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

        /// <summary>
        /// Код родительского отзыва
        /// </summary>
        [ForeignKey("parent")]
        public Guid? parent_id { get; set; }

        /// <summary>
        /// Родительский отзыв
        /// </summary>
        public contractor_response parent { get; set; } = null!;
        #endregion
    }
}
