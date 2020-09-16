using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Models.secr
{
    public class role
    {
        /// <summary>
        /// Код роли
        /// </summary>
        [Key]
        [NotNull]
        public Guid role_id { get; set; }

        /// <summary>
        /// Наименование роли
        /// </summary>
        [NotNull]
        public string role_name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Список пользователей
        /// </summary>
        [JsonIgnore]
        public ICollection<user> users { get; set; }
    }
}
