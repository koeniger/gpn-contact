using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Dto.Directories
{
    /// <summary>
    /// Отображение результатов поиска в контексте директорий
    /// </summary>
    public class DirectoryResultGroupDto
    {
        /// <summary>
        /// Ид директории
        /// </summary>
        public string DirectoryId { get; set; }
        /// <summary>
        /// Наименование группы
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Количество всех позиций для данной директирии (рекурсивно)
        /// </summary>
        public int TotalCount { get; set; }
    }
}
