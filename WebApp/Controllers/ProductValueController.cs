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
    public class ProductValueController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;


        public ProductValueController(Orchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        #region Get

        /// <summary>
        /// Формируется справочник по Продуктам
        /// </summary>
        [HttpGet("product")]
        public async Task<ActionResult<IEnumerable<product_value>>> Get(Guid? product = null)
        {
            if (product != null)
            {
                var result = await _orchestrator.GetProductsValuesByProduct((Guid)product);
                if (result != null && result.Count() > 0) return Ok(result);

                return NotFound();
            }
            var all = await _orchestrator.GetAllProductsValues();
            if (all != null) return Ok(all);

            return NotFound();
        }

        /// <summary>
        /// Формируется справочник по свойствам продукции
        /// </summary>
        [HttpGet("product_property/{id}")]
        public async Task<ActionResult<IEnumerable<product_value>>> GetProductsProperty(Guid id)
        {
            var result = await _orchestrator.GetProductsValuesByProperty(id);
            if (result != null && result.Count() > 0) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Величина Продукта по Id
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _orchestrator.GetProductValue(id);

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
                    var result = await _orchestrator.Add(productValue);

                    if (result != null && result.State == EntityState.Added)
                    {
                        try
                        {
                            await _orchestrator.SaveChangesAsync();
                            var response = await _orchestrator.GetProductValue(result.Entity.product_value_id);

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
        public async Task<ActionResult> Put(Guid id, product_value productValue)
        {
            if (ModelState.IsValid)
            {
                var value = await _orchestrator.GetProductValue(id);

                if (value != null)
                {
                    productValue.product_property_id = id;
                    string proplem = ConstraintsTest(productValue).Result;
                    if (proplem == "")
                    {
                        var result = _orchestrator.Update(productValue);

                        if (result != null && result.State == EntityState.Modified)
                        {
                            await _orchestrator.SaveChangesAsync();

                            var response = await _orchestrator.GetProductValue(result.Entity.product_property_id);

                            return Ok(response);
                        }
                        return NotFound("Свойства продукта не найден");
                    }
                    return NotFound($"{proplem}");
                }
                else
                {
                    return NotFound($"Значение продукта {id} не найден");
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Проверка на существования связанных сущностей в БД
        /// </summary>
        private async Task<string> ConstraintsTest(product_value productValue)
        {
            var product_property = await _orchestrator.GetProductsProperties(productValue.product_property_id);

            var product = await _orchestrator.GetProduct(productValue.product_id);

            string proplem = string.Format("{0}{1}"
                        , product == null ? "Не указан продукт. " : ""
                        , product_property == null ? "Не указаны свойства продукта. " : "");
            return proplem;
        }
        #endregion

        #region Delete
        // DELETE api/<ProductPropertiesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _orchestrator.GetProductValue(id);

            if (result != null)
            {
                _orchestrator.Remove(result);
                var response = await _orchestrator.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Значение продукта {id} не найдено");
        }
        #endregion
    }
}
