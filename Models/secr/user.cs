using Models.gpn;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Models.secr
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class user
    {
        /// <summary>
        /// Код пользователя
        /// </summary>
        [Key]
        [NotNull]
        public Guid user_id { get; set; }

        /// <summary>
        /// ID для входа в систему
        /// </summary>
        [NotNull]
        public string email { get; set; }

        /// <summary>
        /// Наименование пользователя (ФИО)
        /// </summary>
        [NotNull]
        public string user_name { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string position { get; set; }

        /// <summary>
        /// Любая контактная информация
        /// </summary>
        public string contact_info { get; set; }

        #region CONSTRAINT
        /// <summary>
        /// Код раздела справочника продукции
        /// </summary>
        [ForeignKey("role")]
        [NotNull]
        public Guid role_id { get; set; }
        /// <summary>
        /// Раздел
        /// </summary>
        [NotNull]
        public role role { get; set; }

        /// <summary>
        /// Код поставщика
        /// </summary>
        [ForeignKey("contractor")]
        public Guid contractor_id { get; set; }
        /// <summary>
        /// Поставщика
        /// </summary>
        public contractor contractor { get; set; }

        #endregion

        [JsonIgnore]
        public Guid Password { get; set; }
    }
}
