using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.gpn;

namespace WebApp.Models
{
    public class ContactContext : DataContext.ContactContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {
        }

        #region Products method
        /// <summary>
        /// Получает объект Product из бд
        /// </summary>
        public async Task<product> GetProduct(int id) => await this.fdc_products.Include(u => u.product_type).FirstOrDefaultAsync(i => i.product_id == id);

        /// <summary>
        /// Поиск строки в Products по имени, описанию
        /// </summary>
        /// <returns>Product</returns>
        public async Task<IEnumerable<product>> SearchProduct(string search) => await this.fdc_products.Include(u => u.product_type).Where(
                t => t.description_full.ToLower().Contains(search)
                    || t.description_short.ToLower().Contains(search)
                    || t.product_name.ToLower().Contains(search)).ToListAsync();

        #endregion

        #region Products Values methods
        /// <summary>
        /// Получает объект Product из бд
        /// </summary>
        public async Task<product_value> GetProductValue(int id) => await this.fdc_products_values.Include(u => u.product).Include(u => u.product_property).FirstOrDefaultAsync(i => i.product_value_id == id);

        #endregion

        #region ProductsProperties methods
        public async Task<product_property> GetProductsProperties(int id) => await this.fdc_products_properties.FirstOrDefaultAsync(i => i.product_property_id == id);

        public async Task<IEnumerable<product_property>> SearchProductProperty(string search) => await this.fdc_products_properties.Include(u => u.product_type).Include(u => u.okei).Where(
                t => t.property_name.ToLower().Contains(search)).ToListAsync();
        #endregion

        #region Product Types methods 
        /// <summary>
        /// Получает объект ProductType из бд
        /// </summary>
        public async Task<product_type> GetProductType(int? id) => await this.fdc_products_types.FirstOrDefaultAsync(i => i.product_type_id == id);

        /// <summary>
        /// Поиск строки в ProductTypes по имени, описанию
        /// </summary>
        /// <returns>ProductType</returns>
        public async Task<IEnumerable<product_type>> SearchProductType(string search)=> await this.fdc_products_types.Where(t => t.product_type_name.ToLower().Contains(search.ToLower())).ToListAsync();
        #endregion

        #region Contractor methods 
        /// <summary>
        /// Получает объект ProductType из бд
        /// </summary>
        public async Task<contractor> GetContractor(int? id) => await this.fdc_contractors.FirstOrDefaultAsync(i => i.contractor_id == id);

        public async Task<IEnumerable<contractor>>  GetAllContractor() => await this.fdc_contractors.ToListAsync();
        #endregion

        #region Directory methods
        /// <summary>
        /// Получает объект ProductType из бд
        /// </summary>
        public async Task<product_directory> GetDirectory(int? id) => await this.fdc_product_directories.FirstOrDefaultAsync(i => i.product_directory_id == id);

        public async Task<IEnumerable<product_directory>> GetDirectories() => await this.fdc_product_directories.ToListAsync();
        #endregion

        #region OKEI methods
        /// <summary>
        /// Получает объект Product из бд
        /// </summary>
        public async Task<okei> GetOKEI(int id) => await this.fdc_okei.FirstOrDefaultAsync(i => i.okei_id == id);

        public async Task<IEnumerable<okei>> SearchOKEI(string search) => await this.fdc_okei.Where(
                t => t.name_full.ToLower().Contains(search) 
                || t.name_short.ToLower().Contains(search)).ToListAsync();
        #endregion

        #region Image methods
        public async Task<image> GetImage(int id) => await this.fdc_images.FirstOrDefaultAsync(i => i.image_id == id);
        #endregion
    }
}
