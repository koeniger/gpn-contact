using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.gpn;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Контроллер для работы со справочником ProductTypes (Продукты)
    ///<para>GET 	/api/ProductTypes : Get all ProductTypes</para>
    ///<para>GET 	/api/ProductTypes?id= : Get ProductType by id </para>
    ///<para>GET 	/api/ProductTypes/search/{search} : Get all products containing a string </para>
    ///<para>GET 	/api/ProductTypes/{id} : Get the device information identified by "id"</para>
    ///<para>POST   /api/ProductTypes : Create a new device</para>
    ///<para>PUT 	/api/ProductTypes/{id} : Update the device information identified by "id"</para>
    ///<para>DELETE	/api/ProductTypes/{id} : Delete device by "id"</para>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;


        public ProductTypesController(Orchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        #region Get

        /// <summary>
        /// Тип продукта по Id
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get(Guid? id)
        {
            if (id != null)
            {
                var result = await _orchestrator.GetProductType((Guid)id);

                if (result != null) return Ok(result);
            }
            else
            {
                var result = await _orchestrator.GetAllProductType();

                if (result != null) return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// Поиск по полям Products
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<product_type>>> Get(string search)
        {
            var result = await _orchestrator.SearchProductType(search);

            if (result != null && result.Count() > 0) return Ok(result);

            return NotFound();
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> Post(product_type type)
        {
            if (ModelState.IsValid)
            {
                var result = await _orchestrator.Add(type);

                if (result != null && result.State == EntityState.Added)
                {
                    await _orchestrator.SaveChangesAsync();
                    var response = await _orchestrator.GetProductType(result.Entity.product_type_id);
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, product_type type)
        {
            if (ModelState.IsValid)
            {
                type.product_type_id = id;

                var result = _orchestrator.Update(type);

                if (result != null && result.State == EntityState.Modified)
                {
                    await _orchestrator.SaveChangesAsync();
                    var response = await _orchestrator.GetProductType(result.Entity.product_type_id);
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _orchestrator.GetProductType(id);

            if (result != null)
            {
                _orchestrator.Remove(result);
                var response = await _orchestrator.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound();
        }
        #endregion
    }
}
