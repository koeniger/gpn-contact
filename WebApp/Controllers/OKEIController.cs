using System;
using System.Collections.Generic;
using System.Linq;
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
    public class OKEIController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;


        public OKEIController(Orchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        #region Get
        /// <summary>
        /// OKEI по Id
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<okei>>> Get(Guid? id = null)
        {
            if (id != null)
            {
                var result = await _orchestrator.GetOKEI((Guid)id);

                if (result != null) return Ok(result);
            }
            var all = await _orchestrator.GetAllOKEI();
            if (all != null) return Ok(all);

            return NotFound();
        }

        /// <summary>
        /// Поиск по полям OKEI
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<okei>>> Get(string search)
        {
            search = search.ToLower();

            var result = await _orchestrator.SearchOKEI(search);

            if (result != null && result.Count() > 0) return Ok(result);

            return NotFound();
        }
        #endregion

        #region Post - Добавление объекта в БД
        // POST api/<OKEIController>
        [HttpPost]
        public async Task<ActionResult> Post(okei okei)
        {
            if (ModelState.IsValid)
            {
                var result = await _orchestrator.Add(okei);

                if (result != null && result.State == EntityState.Added)
                {
                    try
                    {
                        await _orchestrator.SaveChangesAsync();
                        var response = await _orchestrator.GetOKEI(result.Entity.okei_id);

                        return Ok(response);
                    }
                    catch
                    { }
                }
                return NotFound($"{result.State}");
            }
            return BadRequest();
        }
        #endregion

        #region Put - Изменение объекта в БД
        /// <summary>
        /// Изменение объекта в БД
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, okei okei)
        {
            if (ModelState.IsValid)
            {
                okei.okei_id = id;

                var result = _orchestrator.Update(okei);

                if (result != null && result.State == EntityState.Modified)
                {
                    await _orchestrator.SaveChangesAsync();

                    var response = await _orchestrator.GetOKEI(result.Entity.okei_id);

                    return Ok(response);
                }
                return NotFound($"{result.State}");
            }
            return BadRequest();
        }
        #endregion

        #region Delete - Удаление объекта из БД по Id
        /// <summary>
        /// Удаление объекта из БД по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _orchestrator.GetOKEI(id);

            if (result != null)
            {
                _orchestrator.Remove(result);
                var response = await _orchestrator.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Классификатор {id} не найден");
        }
        #endregion
    }
}
