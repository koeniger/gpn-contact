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
    public class ImageController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly ContactContext dbContext;


        public ImageController(ContactContext context)
        {
            dbContext = context;
        }

        [HttpGet("id")]
        public async Task<ActionResult<image>> Get(int? id = null)
        {
            if (id != null)
            {
                var image = await dbContext.GetImage((int)id);
                if (image != null) return Ok(image);
                else return NotFound();
            }
            else
            {
                var images = await dbContext.fdc_images.ToListAsync();
                if (images != null && images.Count > 0) return Ok(images);
                else return NotFound();
            }

        }

        // GET: api/<ImageController>
        [HttpGet("{table}/{id}")]
        public async Task<ActionResult<IEnumerable<ActionResult>>> Get(string table, int id)
        {
            var image = await dbContext.fdc_images.Where(n => n.any_table_name == table && n.any_table_id == id).ToListAsync();

            if (image != null) return Ok(image);
            else return NotFound();
        }


        // GET api/<ImageController>/5
        [HttpGet("Path/{id}")]
        public async Task<ActionResult<image>> GetPath(int id)
        {
            var image = await dbContext.GetImage(id);

            if (image != null) return Ok(image.image_path);
            else return NotFound();
        }
        // POST api/<ImageController>
        [HttpPost]
        public async Task<ActionResult> Post(image value)
        {
            if (ModelState.IsValid)
            {
                value.image_id = 0;

                var result = await dbContext.fdc_images.AddAsync(value);

                if (result != null && result.State == EntityState.Added)
                {
                    try
                    {
                        await dbContext.SaveChangesAsync();
                        var response = await dbContext.GetImage(result.Entity.image_id);

                        return Ok(response);
                    }
                    catch
                    { }
                }
                return NotFound($"{result.State}");
            }
            return BadRequest();
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, image value)
        {
            if (ModelState.IsValid)
            {
                value.image_id = id;

                var result = dbContext.fdc_images.Update(value);
                string error;
                if (result != null && result.State == EntityState.Modified)
                {
                    try
                    {
                        await dbContext.SaveChangesAsync();
                        var response = await dbContext.GetImage(result.Entity.image_id);

                        return Ok(response);
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                }
                else error = result == null ? "Объект не найден" : result.State.ToString();
                
                return NotFound($"{error}");
            }
            return BadRequest();
        }

        #region Delete - Удаление объекта из БД по Id
        /// <summary>
        /// Удаление объекта из БД по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await dbContext.GetImage(id);

            if (result != null)
            {
                dbContext.fdc_images.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Изображение {id} не найдено");
        }
        #endregion
    }
}
