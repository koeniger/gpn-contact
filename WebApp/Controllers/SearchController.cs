using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Dto.Search;
using Contact.Orchestrators.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpPost("searchProdByFilter")]
        public async Task<ActionResult> SearchProductsByFilter(SearchRequestObjectDto filter)
        {
            ISearchOrchestartor orch = null;//TODO заинжектить в конструкторе реализацию!!!
            var res = await orch.SearchProductsByFilter(filter);
            return Ok(res);
        }

        [HttpPost("searchGroupsByDirectory")]
        public async Task<ActionResult> SearchGroupsByDirectory(int? directoryId)
        {
            ISearchOrchestartor orch = null;//TODO заинжектить в конструкторе реализацию!!!
            var res = await orch.SearchGroupsByDirectory(directoryId);
            return Ok(res);
        }
    }
}
