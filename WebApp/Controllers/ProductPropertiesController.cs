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
    public class ProductPropertiesController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly ContactContext dbContext;


        public ProductPropertiesController(ContactContext context)
        {
            dbContext = context;
        }

        #region GET
         // GET api/<ProductPropertiesController>/5
        [HttpGet("id")]
        public async Task<ActionResult<product_property>> Get(int? id = null)
        {
            if (id != null)
            {
                var result = await dbContext.GetProductsProperties((int)id);

                if (result != null) return Ok(result);
            }
            else
            {
                var products_types = await dbContext.fdc_products_properties.ToListAsync();
                if (products_types != null && products_types.Count > 0) return Ok(products_types);
            }

            return NotFound();
        }

        /// <summary>
        /// Поиск по полям Products
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<product_property>>> Get(string search)
        {
            search = search.ToLower();

            var result = await dbContext.SearchProductProperty(search);

            if (result != null && result.Count() > 0) return Ok(result);

            return NotFound();
        }
        #endregion

        #region POST
        // POST api/<ProductPropertiesController>
        [HttpPost]
        public async Task<ActionResult> Post(product_property productProperty)
        {
            if (ModelState.IsValid)
            {
                string proplem = ConstraintsTest(productProperty).Result;
                if (proplem == "")
                {
                    var result = await dbContext.fdc_products_properties.AddAsync(productProperty);

                    if (result != null && result.State == EntityState.Added)
                    {
                        try
                        {
                            await dbContext.SaveChangesAsync();
                            var response = await dbContext.GetProductsProperties(result.Entity.product_property_id);

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
        public async Task<ActionResult> Put(int id, product_property productProperty)
        {
            if (ModelState.IsValid)
            {
                productProperty.product_property_id = id;
                string proplem = ConstraintsTest(productProperty).Result;
                if (proplem == "")
                {
                    var result = dbContext.fdc_products_properties.Update(productProperty);

                    if (result != null && result.State == EntityState.Modified)
                    {
                        await dbContext.SaveChangesAsync();

                        var response = await dbContext.GetProductsProperties(result.Entity.product_property_id);

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
        private async Task<string> ConstraintsTest(product_property productProperty)
        {
            var product_type = await dbContext.GetProductType(productProperty.product_type_id);

            var okei = await dbContext.GetOKEI(productProperty.okei_id);

            string proplem = string.Format("{0}{1}"
                        , product_type == null ? "Не указан тип продукта. " : ""
                        , okei == null ? "Не указаны Единицы измерения " : "");
            return proplem;
        }
        #endregion

        #region Delete
        // DELETE api/<ProductPropertiesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await dbContext.GetProductsProperties(id);

            if (result != null)
            {
                dbContext.fdc_products_properties.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Свойства продукта {id} не найден");
        }
        #endregion
    }
}
