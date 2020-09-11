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
        private readonly ContactContext dbContext;


        public OKEIController(ContactContext context)
        {
            dbContext = context;
        }
        #region Get
        /// <summary>
        /// OKEI по Id
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<IEnumerable<okei>>> Get(int? id = null)
        {
            if (id != null)
            {
                var result = await dbContext.GetOKEI((int)id);

                if (result != null) return Ok(result);
            }
            var all = await dbContext.fdc_okei.ToListAsync();
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

            var result = await dbContext.SearchOKEI(search);

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
                var result = await dbContext.fdc_okei.AddAsync(okei);

                if (result != null && result.State == EntityState.Added)
                {
                    try
                    {
                        await dbContext.SaveChangesAsync();
                        var response = await dbContext.GetOKEI(result.Entity.okei_id);

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
        public async Task<ActionResult> Put(int id, okei okei)
        {
            if (ModelState.IsValid)
            {
                okei.okei_id = id;

                var result = dbContext.fdc_okei.Update(okei);

                if (result != null && result.State == EntityState.Modified)
                {
                    await dbContext.SaveChangesAsync();

                    var response = await dbContext.GetOKEI(result.Entity.okei_id);

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
        public async Task<ActionResult> Delete(int id)
        {
            var result = await dbContext.GetOKEI(id);

            if (result != null)
            {
                dbContext.fdc_okei.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Классификатор {id} не найден");
        }
        #endregion
    }
}
