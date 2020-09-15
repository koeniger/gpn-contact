using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Dto.Search
{
    /// <summary>
    /// Объект фильтрации продуктов
    /// </summary>
    public class SearchRequestObjectDto
    {
        /// <summary>
        /// Сколько пропустить элементов
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// Сколько взять
        /// </summary>
        public int Take { get; set; }
        /// <summary>
        /// Вернуть ли общее количество
        /// </summary>
        public bool RequestTotalCount { get; set; }
        /// <summary>
        /// Текст поиска в свободной форме
        /// </summary>
        public string FilterText { get; set; }
        /// <summary>
        /// Ид директории в которой ищем (опционально)
        /// </summary>
        public int? DirectoryId { get; set; }
    }
}
