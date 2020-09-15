using Contact.Dto.Search;
using Contact.Orchestrators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchOrchestartor _searchOrchestrator;
        public SearchController(ISearchOrchestartor searchOrchestrator)
        {
            _searchOrchestrator = searchOrchestrator;
        }

        [HttpPost("searchProdByFilter")]
        public async Task<ActionResult> SearchProductsByFilter(SearchRequestObjectDto filter)
        {
            var res = await _searchOrchestrator.SearchProductsByFilter(filter);
            return Ok(res);
        }

        [HttpPost("searchGroupsByDirectory")]
        public async Task<ActionResult> SearchGroupsByDirectory(int? directoryId)
        {
            var res = await _searchOrchestrator.SearchGroupsByDirectory(directoryId);
            return Ok(res);
        }
    }
}
