using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Dto.Directories
{
    /// <summary>
    /// Раздел справочника для ленивой загруки
    /// </summary>
    public class DirectoryLazyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
    }
}
