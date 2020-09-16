using System;

namespace Contact.Dto.Directories
{
    /// <summary>
    /// Раздел справочника
    /// </summary>
    public class DirectoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public DirectoryDto[] Children { get; set; }
    }
}
