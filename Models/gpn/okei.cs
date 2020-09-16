

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models.gpn
{
    /// <summary>
    /// Общероссийский классификатор Единиц измерения
    /// </summary>
    public class okei
    {
        /// <summary>
        /// Код единицы измерения
        /// </summary>
        [Key]
        [NotNull]
        public Guid okei_id { get; set; }

        /// <summary>
        /// Код справочника ОКЕИ
        /// </summary>
        [NotNull]
        public int okei_code { get; set; }

        /// <summary>
        /// Название единиц измерения
        /// </summary>
        [NotNull]
        public string okei_name { get; set; }

        /// <summary>
        /// Национальное условное обозначение
        /// </summary>
        public string symbol_ru { get; set; }

        /// <summary>
        /// Междунаролное условное обозначение
        /// </summary>
        public string symbol_all { get; set; }

        /// <summary>
        /// Национальное кодовое обозначение
        /// </summary>
        public string symbol_ru_code { get; set; }

        /// <summary>
        /// Междунаролное кодовое обозначение
        /// </summary>
        public string symbol_all_code { get; set; }

        /// <summary>
        /// используется в системе
        /// </summary>
        [NotNull]
        public bool is_used { get; set; }
    }
}
