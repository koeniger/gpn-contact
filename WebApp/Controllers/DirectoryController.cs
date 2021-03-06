﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.gpn;
using WebApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly ContactContext dbContext;


        public DirectoryController(ContactContext context)
        {
            dbContext = context;
        }

        #region GET
        /// <summary>
        /// Все разделы
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await dbContext.GetDirectories();

            if (result != null) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Раздел
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int? id)
        {
            if (id != null)
            {
                var result = await dbContext.GetDirectory((int)id);

                if (result != null) return Ok(result);

                return NotFound();
            }

            return await Get();
        }
        #endregion

        #region POST
        /// <summary>
        /// Добавление нового раздела
        ///  POST api/DirectoryController
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post(product_directory new_directory)
        {
            if (ModelState.IsValid)
            {
                string problem = ConstraintsTest(new_directory).Result;

                if (problem != null)
                {

                    var result = await dbContext.fdc_product_directories.AddAsync(new_directory);

                    if (result != null && result.State == EntityState.Added)
                    {
                        await dbContext.SaveChangesAsync();
                        var response = await dbContext.GetDirectory(result.Entity.product_directory_id);
                        return Ok(response);
                    }
                    return NotFound();
                }
                return NotFound(problem);
            }
            return BadRequest();
        }
        #endregion

        #region PUT
        /// <summary>
        /// Редактирование рахдела
        /// PUT api/DirectoryController/{id}
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, product_directory directory)
        {
            if (ModelState.IsValid)
            {
                directory.product_directory_id = id;

                string problem = ConstraintsTest(directory).Result;

                if (problem != null)
                {
                    var result = dbContext.fdc_product_directories.Update(directory);

                    if (result != null && result.State == EntityState.Modified)
                    {
                        await dbContext.SaveChangesAsync();
                        var response = await dbContext.GetDirectory(result.Entity.product_directory_id);
                        return Ok(response);
                    }
                    return NotFound();
                }
                return NotFound(problem);
            }
            return BadRequest();
        }

        /// <summary>
        /// Проверка на существования связанных сущностей в БД
        /// </summary>
        private async Task<string> ConstraintsTest(product_directory directory)
        {
            if (directory.parent_id == 0)
            {
                directory.parent_id = null;
                directory.parent = null;
            }
            else
            {
                directory.parent = await dbContext.GetDirectory(directory.parent_id);
                if (directory.parent == null) return "Не найден родительский раздел";
            }

            return "";
        }
        #endregion

        #region Delete
        /// <summary>
        /// Удаление раздела из бд
        /// DELETE api/DirectoryController/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await dbContext.GetDirectory(id);

            if (result != null)
            {
                dbContext.fdc_product_directories.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(id);
            }
            return NotFound();
        }
        #endregion
    }
}
