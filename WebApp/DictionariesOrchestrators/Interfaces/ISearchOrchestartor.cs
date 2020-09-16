using Contact.Dto.Directories;
using Contact.Dto.Products;
using Contact.Dto.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Orchestrators.Interfaces
{
    public interface ISearchOrchestartor
    {
        /// <summary>
        /// Возвращает сгруппированный результат по родительской папке (выдввается для промежуточных директорий, НЕ для листьев)
        /// </summary>
        /// <param name="directoryId">ид директории</param>
        /// <returns></returns>
        Task<DirectoryResultGroupDto[]> SearchGroupsByDirectory(Guid? directoryId);

        /// <summary>
        /// Возвращает результат поиска продуктов (материалов и оборудования)
        /// </summary>
        /// <param name="filterObj">объект фильтрации</param>
        /// <returns></returns>
        Task<ProductsSearchResultDto> SearchProductsByFilter(SearchRequestObjectDto filterObj);
    }
}
