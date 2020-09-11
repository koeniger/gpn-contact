using Models.gpn;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {
        }
        
        
        #region --------------Dictionary -----------------

       
        /// <summary>
        /// Справочник материалов и оборудования
        /// </summary>
        public DbSet<product> fdc_products { get; set; }

        /// <summary>
        /// Справочник материалов и оборудования
        /// </summary>
        public DbSet<product_value> fdc_products_values { get; set; }

        /// <summary>
        /// Справочник свойств (атрибутов) материалов и оборудования
        /// </summary>
        public DbSet<product_property> fdc_products_properties { get; set; }

        /// <summary>
        /// Справочник типов продукции
        /// </summary>
        public DbSet<product_type> fdc_products_types { get; set; }

        /// <summary>
        /// Общероссийский классификатор единиц измерения
        /// </summary>
        public DbSet<okei> fdc_okei { get; set; }

        /// <summary>
        /// Общее хранилище изображений для всех справочников
        /// </summary>
        public DbSet<image> fdc_images { get; set; }

        /// <summary>
        /// Справочник контрагентов (поставщиков)
        /// </summary>
        public DbSet<contractor> fdc_contractors{ get; set; }

        /// <summary>
        /// Разделы справочника материалов и оборудования
        /// </summary>
        public DbSet<product_directory> fdc_product_directories { get; set; }
        #endregion
    }
}
