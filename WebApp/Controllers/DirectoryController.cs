using Contact.Orchestrators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.gpn;
using System;
using System.Threading.Tasks;
using WebApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;
        private readonly IDirectoryOrchestrator _directoryOrchestrator;

        public DirectoryController(Orchestrator orchestrator, IDirectoryOrchestrator directoryOrchestrator)
        {
            _orchestrator = orchestrator;
            _directoryOrchestrator = directoryOrchestrator;
        }

        [HttpGet("getByParent")]
        public async Task<ActionResult> GetDirectoryByParent(Guid? parentId)
        {
            var res = await _directoryOrchestrator.GetDirectoriesByParent(parentId);
            return Ok(res);
        }

        #region GET
        /// <summary>
        /// Все разделы
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //TODO удалить или переделать метод, он возвращает прямо модели
            var result = await _orchestrator.GetDirectories();

            if (result != null) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Раздел
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid? id)
        {
            if (id != null)
            {
                var result = await _orchestrator.GetDirectory((Guid)id);

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

                    var result = await _orchestrator.Add(new_directory);

                    if (result != null && result.State == EntityState.Added)
                    {
                        await _orchestrator.SaveChangesAsync();
                        var response = await _orchestrator.GetDirectory(result.Entity.product_directory_id);
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
        public async Task<ActionResult> Put(Guid id, product_directory directory)
        {
            if (ModelState.IsValid)
            {
                directory.product_directory_id = id;

                string problem = ConstraintsTest(directory).Result;

                if (problem != null)
                {
                    var result = _orchestrator.Update(directory);

                    if (result != null && result.State == EntityState.Modified)
                    {
                        await _orchestrator.SaveChangesAsync();
                        var response = await _orchestrator.GetDirectory(result.Entity.product_directory_id);
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
            if (directory.parent_id == null)
            {
                directory.parent_id = null;
                directory.parent = null;
            }
            else
            {
                directory.parent = await _orchestrator.GetDirectory(directory.parent_id);
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
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _orchestrator.GetDirectory(id);

            if (result != null)
            {
                _orchestrator.Remove(result);
                var response = await _orchestrator.SaveChangesAsync();

                return Ok(id);
            }
            return NotFound();
        }
        #endregion
    }
}
