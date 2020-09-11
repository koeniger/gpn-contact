

using System.ComponentModel.DataAnnotations;

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
        public int okei_id { get; set; }

        /// <summary>
        /// Наимменование сокращенное
        /// </summary>
        public string name_short { get; set; }

        /// <summary>
        /// Наимменование полное
        /// </summary>
        public string name_full { get; set; }
    }
}
