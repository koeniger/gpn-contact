using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.gpn;
using WebApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {

        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;


        public ContractorController(Orchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        #region GET
        // GET: api/<ContractorController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _orchestrator.GetAllContractor();

            if (result != null) return Ok(result);

            return NotFound();
        }

        // GET api/<ContractorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid? id)
        {
            if (id != null)
            {
                var result = await _orchestrator.GetContractor((Guid)id);

                if (result != null) return Ok(result);

                return NotFound();
            }

            return await Get();
        }
        #endregion

        #region POST
        // POST api/<ContractorController>
        [HttpPost]
        public async Task<ActionResult> Post(contractor new_contractor)
        {
            if (ModelState.IsValid)
            {
                var result = await _orchestrator.Add(new_contractor);

                if (result != null && result.State == EntityState.Added)
                {
                    await _orchestrator.SaveChangesAsync();
                    var response = await _orchestrator.GetContractor(result.Entity.contractor_id);
                    return Ok(response);
                }
                return NotFound();
            }
            return BadRequest();
        }
        #endregion

        #region GET
        // PUT api/<ContractorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, contractor modify_contractor)
        {
            if (ModelState.IsValid)
            {
                modify_contractor.contractor_id = id;

                var result = _orchestrator.Update(modify_contractor);

                if (result != null && result.State == EntityState.Modified)
                {
                    await _orchestrator.SaveChangesAsync();
                    var response = await _orchestrator.GetContractor(result.Entity.contractor_id);
                    return Ok(response);
                }
                return NotFound();
            }
            return BadRequest();
        }
        #endregion

        #region Delete

        // DELETE api/<ContractorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _orchestrator.GetContractor(id);

            if (result != null)
            {
                _orchestrator.Remove(result);

                var response = await _orchestrator.SaveChangesAsync();

                return Ok(id);
            }
            return NotFound();
        }
        #endregion
    }
}
