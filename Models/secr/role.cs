using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.secr
{
    public class role
    {
        /// <summary>
        /// Код роли
        /// </summary>
        [Key]
        public int role_id { get; set; }

        /// <summary>
        /// Наименование роли
        /// </summary>
        string role_name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        string description { get; set; }

        /// <summary>
        /// Список пользователей
        /// </summary>
        [JsonIgnore]
        public ICollection<user> users { get; set; }
    }
}
