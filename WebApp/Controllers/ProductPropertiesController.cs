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
    public class ProductPropertiesController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;


        public ProductPropertiesController(Orchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        #region GET
         // GET api/<ProductPropertiesController>/5
        [HttpGet("id")]
        public async Task<ActionResult<product_property>> Get(Guid? id = null)
        {
            if (id != null)
            {
                var result = await _orchestrator.GetProductsProperties((Guid)id);

                if (result != null) return Ok(result);
            }
            else
            {
                var productsProperties = await _orchestrator.GetAllProductsProperties();
                if (productsProperties != null && productsProperties.Count() > 0) return Ok(productsProperties);
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

            var result = await _orchestrator.SearchProductProperty(search);

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
                    var result = await _orchestrator.Add(productProperty);

                    if (result != null && result.State == EntityState.Added)
                    {
                        try
                        {
                            await _orchestrator.SaveChangesAsync();
                            var response = await _orchestrator.GetProductsProperties(result.Entity.product_property_id);

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
        public async Task<ActionResult> Put(Guid id, product_property productProperty)
        {
            if (ModelState.IsValid)
            {
                var p = await _orchestrator.GetProductsProperties(id);

                if (p != null)
                {
                    productProperty.product_property_id = id;
                    string proplem = ConstraintsTest(productProperty).Result;
                    if (proplem == "")
                    {
                        var result = _orchestrator.Update(productProperty);

                        if (result != null && result.State == EntityState.Modified)
                        {
                            await _orchestrator.SaveChangesAsync();

                            var response = await _orchestrator.GetProductsProperties(result.Entity.product_property_id);

                            return Ok(response);
                        }
                        return NotFound("Свойства продукта не найдены");
                    }
                    else
                    {
                        return NotFound($"{proplem}");
                    }
                }
                else
                {
                    return NotFound($"Свойство продукта {id} не найдено");
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Проверка на существования связанных сущностей в БД
        /// </summary>
        private async Task<string> ConstraintsTest(product_property productProperty)
        {
            var product_type = await _orchestrator.GetProductType(productProperty.product_type_id);

            var okei = await _orchestrator.GetOKEI(productProperty.okei_id);

            string proplem = string.Format("{0}{1}"
                        , product_type == null ? "Не указан тип продукта. " : ""
                        , okei == null ? "Не указаны Единицы измерения " : "");

            if (proplem == "")
            {
                productProperty.product_type = product_type;
                productProperty.okei = okei;
            }
            return proplem;
        }
        #endregion

        #region Delete
        // DELETE api/<ProductPropertiesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _orchestrator.GetProductsProperties(id);

            if (result != null)
            {
                _orchestrator.Remove(result);
                var response = await _orchestrator.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Свойства продукта {id} не найден");
        }
        #endregion
    }
}
