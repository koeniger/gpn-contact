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
    public class ProductValueController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly ContactContext dbContext;


        public ProductValueController(ContactContext context)
        {
            dbContext = context;
        }

        #region Get

        /// <summary>
        /// Формируется справочник по Продуктам
        /// </summary>
        [HttpGet("product")]
        public async Task<ActionResult<IEnumerable<product_value>>> Get(int? product = null)
        {
            if (product != null)
            {
                var result = await dbContext.fdc_products_values.Where(t => t.product_id == product).Include(d => d.product).Include(c => c.product_property).ToListAsync();
                if (result != null && result.Count > 0) return Ok(result);

                return NotFound();
            }
            var all = await dbContext.fdc_products_values.Include(p => p.product).Include(u => u.product_property).ToListAsync();
            if (all != null) return Ok(all);

            return NotFound();
        }

        /// <summary>
        /// Формируется справочник по свойствам продукции
        /// </summary>
        [HttpGet("product_property/{id}")]
        public async Task<ActionResult<IEnumerable<product_value>>> GetProductsDirectory(int id)
        {
            var result = await dbContext.fdc_products_values.Where(t => t.product_property_id == id).Include(d => d.product).ToListAsync();
            if (result != null && result.Count > 0) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Величина Продукта по Id
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await dbContext.GetProductValue(id);

            if (result != null) return Ok(result);

            return NotFound();
        }


        #endregion

        #region POST
        // POST api/<ProductPropertiesController>
        [HttpPost]
        public async Task<ActionResult> Post(product_value productValue)
        {
            if (ModelState.IsValid)
            {
                string proplem = ConstraintsTest(productValue).Result;
                if (proplem == "")
                {
                    var result = await dbContext.fdc_products_values.AddAsync(productValue);

                    if (result != null && result.State == EntityState.Added)
                    {
                        try
                        {
                            await dbContext.SaveChangesAsync();
                            var response = await dbContext.GetProductValue(result.Entity.product_value_id);

                            return Ok(response);
                        }
                        catch
                        { }
                    }
                    return NotFound($"{result.State}");
                }
                else
                {
                    return NotFound($"{proplem}");
                }
            }
            return BadRequest();
        }
        #endregion

        #region PUT
        // PUT api/<ProductPropertiesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, product_value productValue)
        {
            if (ModelState.IsValid)
            {
                productValue.product_property_id = id;
                string proplem = ConstraintsTest(productValue).Result;
                if (proplem == "")
                {
                    var result = dbContext.fdc_products_values.Update(productValue);

                    if (result != null && result.State == EntityState.Modified)
                    {
                        await dbContext.SaveChangesAsync();

                        var response = await dbContext.GetProductValue(result.Entity.product_property_id);

                        return Ok(response);
                    }
                    return NotFound("Свойства продукта не найден");
                }
                else
                {
                    return NotFound($"{proplem}");
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Проверка на существования связанных сущностей в БД
        /// </summary>
        private async Task<string> ConstraintsTest(product_value productValue)
        {
            var product_type = await dbContext.GetProduct(productValue.product_property_id);

            var product = await dbContext.GetProduct(productValue.product_id);

            string proplem = string.Format("{0}{1}"
                        , product == null ? "Не указан продукт. " : ""
                        , product_type == null ? "Не указан тип продукта. " : "");
            return proplem;
        }
        #endregion

        #region Delete
        // DELETE api/<ProductPropertiesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await dbContext.GetProductValue(id);

            if (result != null)
            {
                dbContext.fdc_products_values.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Значение продукта {id} не найдено");
        }
        #endregion
    }
}
