using System;

namespace Contact.Dto.Directories
{
    /// <summary>
    /// Раздел справочника
    /// </summary>
    public class DirectoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public DirectoryDto[] Children { get; set; }
    }
}
