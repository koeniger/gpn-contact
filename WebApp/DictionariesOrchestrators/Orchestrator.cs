using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.gpn;
using Models.secr;
using WebApp.dto;

namespace WebApp.Models
{
    public class Orchestrator
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly ContactContext _context;
        public Orchestrator(ContactContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Сохранение результатов в ДБ
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        #region Products method
        /// <summary>
        /// Получает объект Product из бд
        /// </summary>
        public async Task<product> GetProduct(int id) => await _context.fdc_products.Include(u => u.product_type).FirstOrDefaultAsync(i => i.product_id == id);

        /// <summary>
        /// Поиск строки в Products по имени, описанию
        /// </summary>
        /// <returns>Product</returns>
        public async Task<IEnumerable<product>> SearchProduct(string search) => await _context.fdc_products.Include(u => u.product_type).Where(
                t => t.description_full.ToLower().Contains(search)
                    || t.description_short.ToLower().Contains(search)
                    || t.product_name.ToLower().Contains(search)).ToListAsync();

        /// <summary>
        /// Список продуктов по типу продуктов (product_type)
        /// </summary>
        public async Task<IEnumerable<product>> GetProductsProductsTypes(int? product_type) => await _context.fdc_products.Where(t => t.product_type_id == product_type).Include(d => d.product_directory).ThenInclude(p => p.parent).Include(c => c.contractor).Include(t => t.product_type).ToListAsync();

        /// <summary>
        /// Все продукты
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<product>> GetAllProducts() => await _context.fdc_products.Include(u => u.product_type).ToListAsync();

        /// <summary>
        /// Список продуктов по всему разделу (product_directory)
        /// </summary>
        public async Task<IEnumerable<product>> GetProductsDirectory(int id) => await _context.fdc_products.Where(t => t.product_directory_id == id).Include(d => d.product_directory).ThenInclude(p => p.parent).Include(c => c.contractor).Include(t => t.product_type).ToListAsync();


        /// <summary>
        /// Список продуктов по производителю (contractor)
        /// </summary>
        public async Task<IEnumerable<product>> GetProductsContractor(int id) => await _context.fdc_products.Where(t => t.contractor_id == id).Include(d => d.product_directory).ThenInclude(p => p.parent).Include(c => c.contractor).Include(t => t.product_type).ToListAsync();


        public async Task<EntityEntry<product>> Add(product new_product) => await _context.fdc_products.AddAsync(new_product);

        public EntityEntry<product> Update(product modify_product) => _context.fdc_products.Update(modify_product);

        public EntityEntry<product> Remove(product remove_product) => _context.fdc_products.Remove(remove_product);

        #endregion

        #region Products Values methods
        /// <summary>
        /// Получает объект Product из бд
        /// </summary>
        public async Task<product_value> GetProductValue(int id) => await _context.fdc_products_values.Include(u => u.product).Include(u => u.product_property).FirstOrDefaultAsync(i => i.product_value_id == id);


        public async Task<IEnumerable<product_value>> GetProductsValuesByProduct(int id) => await _context.fdc_products_values.Where(t => t.product_id == id).Include(d => d.product).Include(c => c.product_property).ToListAsync();

        public async Task<IEnumerable<product_value>> GetProductsValuesByProperty(int id) => await _context.fdc_products_values.Where(t => t.product_property_id == id).Include(d => d.product).ToListAsync();

        public async Task<IEnumerable<product_value>> GetAllProductsValues() => await _context.fdc_products_values.Include(p => p.product).Include(u => u.product_property).ToListAsync();


        public async Task<EntityEntry<product_value>> Add(product_value new_value) => await _context.fdc_products_values.AddAsync(new_value);

        public EntityEntry<product_value> Update(product_value modify_value) => _context.fdc_products_values.Update(modify_value);

        public EntityEntry<product_value> Remove(product_value remove_value) => _context.fdc_products_values.Remove(remove_value);

        #endregion

        #region ProductsProperties methods
        public async Task<product_property> GetProductsProperties(int id) => await _context.fdc_products_properties.FirstOrDefaultAsync(i => i.product_property_id == id);

        public async Task<IEnumerable<product_property>> GetAllProductsProperties() => await _context.fdc_products_properties.ToListAsync();

        public async Task<IEnumerable<product_property>> SearchProductProperty(string search) => await _context.fdc_products_properties.Include(u => u.product_type).Include(u => u.okei).Where(
                t => t.property_name.ToLower().Contains(search)).ToListAsync();

        public async Task<EntityEntry<product_property>> Add(product_property new_property) => await _context.fdc_products_properties.AddAsync(new_property);

        public EntityEntry<product_property> Update(product_property modify_property) => _context.fdc_products_properties.Update(modify_property);

        public EntityEntry<product_property> Remove(product_property remove_property) => _context.fdc_products_properties.Remove(remove_property);

        #endregion

        #region Product Types methods 
        /// <summary>
        /// Получает объект ProductType из бд
        /// </summary>
        public async Task<product_type> GetProductType(int? id) => await _context.fdc_products_types.FirstOrDefaultAsync(i => i.product_type_id == id);

        public async Task<IEnumerable<product_type>> GetAllProductType() => await _context.fdc_products_types.ToListAsync();
        /// <summary>
        /// Поиск строки в ProductTypes по имени, описанию
        /// </summary>
        /// <returns>ProductType</returns>
        public async Task<IEnumerable<product_type>> SearchProductType(string search)=> await _context.fdc_products_types.Where(t => t.product_type_name.ToLower().Contains(search.ToLower())).ToListAsync();
        public async Task<EntityEntry<product_type>> Add(product_type new_type) => await _context.fdc_products_types.AddAsync(new_type);

        public EntityEntry<product_type> Update(product_type modify_type) => _context.fdc_products_types.Update(modify_type);

        public EntityEntry<product_type> Remove(product_type remove_type) => _context.fdc_products_types.Remove(remove_type);


        #endregion

        #region Contractor methods 
        /// <summary>
        /// Получает объект ProductType из бд
        /// </summary>
        public async Task<contractor> GetContractor(int? id) => await _context.fdc_contractors.FirstOrDefaultAsync(i => i.contractor_id == id);

        public async Task<IEnumerable<contractor>>  GetAllContractor() => await _context.fdc_contractors.ToListAsync();

        public async Task<EntityEntry<contractor>> Add(contractor new_contractor) => await _context.fdc_contractors.AddAsync(new_contractor);

        public EntityEntry<contractor> Update(contractor modify_contractor) => _context.fdc_contractors.Update(modify_contractor);

        public EntityEntry<contractor> Remove(contractor remove_contractor) => _context.fdc_contractors.Remove(remove_contractor);
        #endregion

        #region Directory methods
        /// <summary>
        /// Получает объект ProductType из бд
        /// </summary>
        public async Task<product_directory> GetDirectory(int? id) => await _context.fdc_product_directories.FirstOrDefaultAsync(i => i.product_directory_id == id);

        public async Task<IEnumerable<product_directory>> GetDirectories() => await _context.fdc_product_directories.ToListAsync();

        public async Task<EntityEntry<product_directory>> Add(product_directory new_directory) => await _context.fdc_product_directories.AddAsync(new_directory);

        public EntityEntry<product_directory> Update(product_directory modify_directory) => _context.fdc_product_directories.Update(modify_directory);

        public EntityEntry<product_directory> Remove(product_directory remove_directory) => _context.fdc_product_directories.Remove(remove_directory);


        #endregion

        #region OKEI methods
        /// <summary>
        /// Получает объект Product из бд
        /// </summary>
        public async Task<okei> GetOKEI(int id) => await _context.fdc_okei.FirstOrDefaultAsync(i => i.okei_id == id);

        public async Task<IEnumerable<okei>> GetAllOKEI() => await _context.fdc_okei.ToListAsync();
        public async Task<IEnumerable<okei>> SearchOKEI(string search) => await _context.fdc_okei.Where(
                t => t.okei_name.ToLower().Contains(search) 
                || t.symbol_ru.ToLower().Contains(search)).ToListAsync();

        public async Task<EntityEntry<okei>> Add(okei new_okei) => await _context.fdc_okei.AddAsync(new_okei);

        public EntityEntry<okei> Update(okei modify_okei) => _context.fdc_okei.Update(modify_okei);

        public EntityEntry<okei> Remove(okei remove_okei) => _context.fdc_okei.Remove(remove_okei);

        #endregion

        #region Image methods
        public async Task<image> GetImage(int id) => await _context.fdc_images.FirstOrDefaultAsync(i => i.image_id == id);

        public async Task<IEnumerable<image>> GetAllImages() => await _context.fdc_images.ToListAsync();

        public async Task<IEnumerable<image>> GetImages(string table, int id) => await _context.fdc_images.Where(n => n.any_table_name == table && n.any_table_id == id).ToListAsync();

        public async Task<EntityEntry<image>> Add(image new_image) => await _context.fdc_images.AddAsync(new_image);

        public EntityEntry<image> Update(image modify_image) => _context.fdc_images.Update(modify_image);

        public EntityEntry<image> Remove(image remove_image) => _context.fdc_images.Remove(remove_image);

        #endregion

        #region Users method
        /// <summary>
        /// Получает объект Product из бд
        /// </summary>
        public async Task<user> GetUser(AuthenticateRequest model, Guid hash) => await _context.fdc_users.Include(u => u.role).SingleOrDefaultAsync(x => x.email == model.UserEmail && x.Password == hash);

        public async Task<IEnumerable<user>> GetUsers() => await _context.fdc_users.Include(u => u.role).ToListAsync();

        public async Task<user> GetUser(int id) => await _context.fdc_users.Include(u => u.role).FirstOrDefaultAsync(x => x.user_id == id);

        public async Task<EntityEntry<user>> Add(user new_user) => await _context.fdc_users.AddAsync(new_user);

        public EntityEntry<user> Update(user modify_user) => _context.fdc_users.Update(modify_user);

        public EntityEntry<user> Remove(user remove_user) => _context.fdc_users.Remove(remove_user);

        #endregion

        #region Role method
        public async Task<role> GetRole(string role_name) => await _context.fdc_roles.FirstOrDefaultAsync(x => x.role_name.ToLower().Contains(role_name));

        public async Task<EntityEntry<role>> Add(role new_role) => await _context.fdc_roles.AddAsync(new_role);

        #endregion
    }
}
