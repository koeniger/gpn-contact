﻿using System.Threading.Tasks;
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
        private readonly ContactContext dbContext;


        public ContractorController(ContactContext context)
        {
            dbContext = context;
        }

        #region GET
        // GET: api/<ContractorController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await dbContext.GetAllContractor();

            if (result != null) return Ok(result);

            return NotFound();
        }

        // GET api/<ContractorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int? id)
        {
            if (id != null)
            {
                var result = await dbContext.GetContractor((int)id);

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
                var result = await dbContext.fdc_contractors.AddAsync(new_contractor);

                if (result != null && result.State == EntityState.Added)
                {
                    await dbContext.SaveChangesAsync();
                    var response = await dbContext.GetContractor(result.Entity.contractor_id);
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
        public async Task<ActionResult> Put(int id, contractor modify_contractor)
        {
            if (ModelState.IsValid)
            {
                modify_contractor.contractor_id = id;

                var result = dbContext.fdc_contractors.Update(modify_contractor);

                if (result != null && result.State == EntityState.Modified)
                {
                    await dbContext.SaveChangesAsync();
                    var response = await dbContext.GetContractor(result.Entity.contractor_id);
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
        public async Task<ActionResult> Delete(int id)
        {
            var result = await dbContext.GetContractor(id);

            if (result != null)
            {
                dbContext.fdc_contractors.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(id);
            }
            return NotFound();
        }
        #endregion
    }
}
