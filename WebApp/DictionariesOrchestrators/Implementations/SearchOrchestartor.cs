using AutoMapper;
using Contact.Dto.Directories;
using Contact.Dto.Search;
using Contact.Orchestrators.Interfaces;
using DataContext;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Contact.Dto.Products;

namespace WebApp.DictionariesOrchestrators.Implementations
{
    public class SearchOrchestartor : ISearchOrchestartor
    {
        private readonly IMapper _mapper;
        private readonly ContactContext _context;

        public SearchOrchestartor(IMapper mapper, ContactContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<DirectoryResultGroupDto[]> SearchGroupsByDirectory(int? directoryId)
        {
            var result = _context.fdc_product_directories
                .Where(d => d.product_directory_id == directoryId)
                .AsQueryable()
                .ProjectTo<DirectoryResultGroupDto>(_mapper.ConfigurationProvider)
                .ToArray();

            return Task.FromResult(result);
        }

        public Task<ProductsSearchResultDto> SearchProductsByFilter(SearchRequestObjectDto filterObj)
        {
            ProductsSearchResultDto result = new ProductsSearchResultDto();

            if ( filterObj.Take < 1 || filterObj.Skip < 0)
            {
                return Task.FromException<ProductsSearchResultDto>(new Exception("Ошибка в запросе фильтрации"));
            }

            var productsQueryable = _context.fdc_products
                .Where(p => p.product_name.Contains(filterObj.FilterText))
                .Skip(filterObj.Skip)
                .Take(filterObj.Take)
                .AsQueryable();

            if (filterObj.DirectoryId != null)
            {
                productsQueryable = productsQueryable.Where(p => p.product_directory_id == filterObj.DirectoryId);
            }

            result.Products = productsQueryable.ProjectTo<ProductShortViewDto>(_mapper.ConfigurationProvider).ToArray();

            if (filterObj.RequestTotalCount)
                result.TotalCount = result.Products.Length;

            return Task.FromResult(result);
        }
    }
}
