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
        private readonly ContactContext dbContext;


        public ProductTypesController(ContactContext context)
        {
            dbContext = context;
        }

        #region Get

        /// <summary>
        /// Тип продукта по Id
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get(int? id)
        {
            if (id != null)
            {
                var result = await dbContext.GetProductType((int)id);

                if (result != null) return Ok(result);
            }
            else
            {
                var result = await dbContext.fdc_products_types.ToListAsync();

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
            var result = await dbContext.SearchProductType(search);

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
                var result = await dbContext.fdc_products_types.AddAsync(type);

                if (result != null && result.State == EntityState.Added)
                {
                    await dbContext.SaveChangesAsync();
                    var response = await dbContext.GetProductType(result.Entity.product_type_id);
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, product_type type)
        {
            if (ModelState.IsValid)
            {
                type.product_type_id = id;

                var result = dbContext.fdc_products_types.Update(type);

                if (result != null && result.State == EntityState.Modified)
                {
                    await dbContext.SaveChangesAsync();
                    var response = await dbContext.GetProductType(result.Entity.product_type_id);
                    return Ok(response);
                }
            }
            return BadRequest();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await dbContext.GetProductType(id);

            if (result != null)
            {
                dbContext.fdc_products_types.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound();
        }
        #endregion
    }
}
