using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.gpn;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;

        private readonly ImageService _imageService;

        public ImageController(Orchestrator orchestrator, ImageService imageService)
        {
            _orchestrator = orchestrator;
            _imageService = imageService;
        }

        [HttpGet("id")]
        public async Task<ActionResult<image>> Get(int? id = null)
        {
            if (id != null)
            {
                var image = await _orchestrator.GetImage((int)id);
                if (image != null) return Ok(image);
                else return NotFound();
            }
            else
            {
                var images = await _orchestrator.GetAllImages();
                if (images != null && images.Count() > 0) return Ok(images);
                else return NotFound();
            }

        }

        // GET: api/<ImageController>
        [HttpGet("{table}/{id}")]
        public async Task<ActionResult<IEnumerable<ActionResult>>> Get(string table, int id)
        {
            var image = await _orchestrator.GetImages(table, id);

            if (image != null) return Ok(image);
            else return NotFound();
        }


        // GET api/<ImageController>/5
        [HttpGet("Path/{id}")]
        public async Task<ActionResult<image>> GetPath(int id)
        {
            var image = await _orchestrator.GetImage(id);

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

                var result = await _orchestrator.Add(value);

                if (result != null && result.State == EntityState.Added)
                {
                    try
                    {
                        await _orchestrator.SaveChangesAsync();
                        var response = await _orchestrator.GetImage(result.Entity.image_id);

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

                var result = _orchestrator.Update(value);
                string error;
                if (result != null && result.State == EntityState.Modified)
                {
                    try
                    {
                        await _orchestrator.SaveChangesAsync();
                        var response = await _orchestrator.GetImage(result.Entity.image_id);

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
            var result = await _orchestrator.GetImage(id);

            if (result != null)
            {
                _orchestrator.Remove(result);
                var response = await _orchestrator.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Изображение {id} не найдено");
        }

        /// <summary>
        /// Загрузка картинки на сервер
        /// </summary>
        [HttpPost("Upload/{table_name}/{table_id:int}/{main:bool}")]
        public async Task<ActionResult> Upload(IFormFile file, string table_name, int table_id, bool main = true)
        {
            if (file != null && file.Length != 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var filePath = await _imageService.SaveImage(file, table_name, table_id);

                        image db_image = new image() {
                            any_table_id = table_id,
                            any_table_name = table_name,
                            image_path = filePath,
                            is_main = main
                        };

                        return await this.Post(db_image);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                }
            }
            return BadRequest();

        }
        #endregion
    }
}
