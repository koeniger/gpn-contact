using Contact.Dto.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Dto.Search
{
    /// <summary>
    /// Результат поиска продуктов
    /// </summary>
    public class ProductsSearchResultDto
    {
        /// <summary>
        /// Перечень найденных продуктов
        /// </summary>
        public ProductShortViewDto[] Products { get; set; }

        /// <summary>
        /// Общее количество найденных элементов (если было запрошено)
        /// </summary>
        public long? TotalCount { get; set; }
    }
}
