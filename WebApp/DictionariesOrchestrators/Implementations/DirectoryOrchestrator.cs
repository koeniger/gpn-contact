using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contact.Dto.Directories;
using Contact.Orchestrators.Interfaces;
using DataContext;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DictionariesOrchestrators.Implementations
{
    public class DirectoryOrchestrator : IDirectoryOrchestrator
    {
        private readonly IMapper _mapper;
        private readonly ContactContext _context;
        public DirectoryOrchestrator(IMapper mapper, ContactContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public Task<DirectoryLazyDto[]> GetDirectoriesByParent(int? parentId)
        {
            var result = _context.fdc_product_directories
                .Where(d => d.parent_id == parentId)
                .AsQueryable()
                .ProjectTo<DirectoryLazyDto>(_mapper.ConfigurationProvider)
                .ToArray();

            return Task.FromResult(result);
        }
    }
}
