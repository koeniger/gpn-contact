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
    /// Контроллер для работы со справочником Product (Продукты)
    ///<para>GET 	/api/Products : Get all devices</para>
    ///<para>GET 	/api/Products?productType= : Get all products of the same type </para>
    ///<para>GET 	/api/Products/search/{search} : Get all products containing a string </para>
    ///<para>GET 	/api/Products/{id} : Get the device information identified by "id"</para>
    ///<para>POST   /api/Products : Create a new device</para>
    ///<para>PUT 	/api/Products/{id} : Update the device information identified by "id"</para>
    ///<para>DELETE	/api/Products/{id} : Delete device by "id"</para>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly ContactContext dbContext;


        public ProductsController(ContactContext context)
        {
            dbContext = context;
        }

        #region Get

        /// <summary>
        /// Формируется справочник по Типу продукции
        /// </summary>
        [HttpGet("product_type")]
        public async Task<ActionResult<IEnumerable<product>>> Get(int? product_type = null)
        {
            if (product_type != null)
            {
                var result = await dbContext.fdc_products.Where(t => t.product_type_id == product_type).Include(d => d.product_directory).ThenInclude(p => p.parent).Include(c => c.contractor).Include(t => t.product_type).ToListAsync();
                if (result != null && result.Count > 0) return Ok(result);

                return NotFound();
            }
            var all = await dbContext.fdc_products.Include(u => u.product_type).ToListAsync();
            if (all != null) return Ok(all);

            return NotFound();
        }

        /// <summary>
        /// Формируется справочник по разделам продукции
        /// </summary>
        [HttpGet("product_directory/{id}")]
        public async Task<ActionResult<IEnumerable<product>>> GetProductsDirectory(int id)
        {
            var result = await dbContext.fdc_products.Where(t => t.product_directory_id == id).Include(d => d.product_directory).ThenInclude(p=>p.parent).Include(c=>c.contractor).Include(t=>t.product_type).ToListAsync();
            if (result != null && result.Count > 0) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Формируется справочник по производителям
        /// </summary>
        [HttpGet("contractor/{id}")]
        public async Task<ActionResult<IEnumerable<product>>> GetProductsContractor(int id)
        {
            var result = await dbContext.fdc_products.Where(t => t.contractor_id == id).Include(d => d.product_directory).ThenInclude(p => p.parent).Include(c => c.contractor).Include(t => t.product_type).ToListAsync();
            if (result != null && result.Count > 0) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Продуск по Id
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await dbContext.GetProduct(id);

            if (result != null) return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Поиск по полям Products
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<product>>> Get(string search)
        {
            search = search.ToLower();

            var result = await dbContext.SearchProduct(search);

            if (result != null && result.Count() > 0) return Ok(result);

            return NotFound();
        }
        #endregion

        #region Post - Добавление объекта в БД
        /// <summary>
        /// Добавление объекта в БД
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(product product)
        {
            if (ModelState.IsValid)
            {
                string proplem = ConstraintsTest(product).Result;
                if (proplem == "")
                {
                    var result = await dbContext.fdc_products.AddAsync(product);

                    if (result != null && result.State == EntityState.Added)
                    {
                        try
                        {
                            await dbContext.SaveChangesAsync();
                            var response = await dbContext.GetProduct(result.Entity.product_id);

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

        /// <summary>
        /// Проверка на существования связанных сущностей в БД
        /// </summary>
        private async Task<string> ConstraintsTest(product product)
        {
            var productType = await dbContext.GetProductType(product.product_type_id);
            var productDirectory = await dbContext.GetDirectory(product.product_directory_id);
            var contractor = await dbContext.GetContractor(product.contractor_id);

            string proplem = string.Format("{0}{1}{2}"
                            , productType == null ? "Тип продукции не существует. " : ""
                            , productDirectory == null ? "Раздел не существует. " : ""
                            , contractor == null ? "Поставщик не существует. " : "");

            if (proplem == "")
            {
                product.product_type = productType;
                product.contractor = contractor;
                product.product_directory = productDirectory;
            }
            return proplem;
        }

        #endregion

        #region Put - Изменение объекта в БД
        /// <summary>
        /// Изменение объекта в БД
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, product product)
        {
            if (ModelState.IsValid)
            {
                product.product_id = id;

                string proplem = ConstraintsTest(product).Result;
                if (proplem == "")
                {
                    var result = dbContext.fdc_products.Update(product);

                    if (result != null && result.State == EntityState.Modified)
                    {
                        await dbContext.SaveChangesAsync();

                        var response = await dbContext.GetProduct(result.Entity.product_id);
                        
                        return Ok(response);
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

        #region Delete - Удаление объекта из БД по Id
        /// <summary>
        /// Удаление объекта из БД по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await dbContext.GetProduct(id);

            if (result != null)
            {
                dbContext.fdc_products.Remove(result);
                var response = await dbContext.SaveChangesAsync();

                return Ok(response);
            }
            return NotFound($"Продукт {id} не найден");
        }
        #endregion
    }
}
