using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.gpn
{
    /// <summary>
    /// Справочник контрагентов (поставщиков)
    /// </summary>
    public class contractor
    {
        /// <summary>
        /// Код поставщика
        /// </summary>
        [Key]
        public int contractor_id { get; set; }

        /// <summary>
        /// Нименование контрагента
        /// </summary>
        public string contractor_name { get; set; }

        /// <summary>
        /// Описание раздела
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Контактная информация
        /// </summary>
        public string contact_info { get; set; }

        [JsonIgnore]
        public ICollection<product> Products { get; set; }

        [JsonIgnore]
        public ICollection<contractor_response> Responses { get; set; }

        [JsonIgnore]
        public ICollection<contractor_rate> Rates { get; set; }
    }
}
