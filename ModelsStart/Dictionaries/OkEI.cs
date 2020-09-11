

using System.ComponentModel.DataAnnotations;

namespace ModelsStart.Dictionaries
{
    /// <summary>
    /// Общероссийский классификатор Единиц измерения
    /// </summary>
    public class OkEI
    {
        /// <summary>
        /// Код единицы измерения
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Наимменование сокращенное
        /// </summary>
        public string NameShort { get; set; }

        /// <summary>
        /// Наимменование полное
        /// </summary>
        public string NameFull { get; set; }
    }
}
