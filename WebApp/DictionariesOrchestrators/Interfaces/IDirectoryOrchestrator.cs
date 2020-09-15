using Contact.Dto.Directories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Orchestrators.Interfaces
{
    /// <summary>
    /// Интерйефс оркестратора для работы с разделами спарвочника
    /// </summary>
    public interface IDirectoryOrchestrator
    {
        Task<DirectoryLazyDto[]> GetDirectoriesByParent(int? parentId);
    }
}
