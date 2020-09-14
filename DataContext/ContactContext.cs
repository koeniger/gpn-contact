using Models.gpn;
using Microsoft.EntityFrameworkCore;
using Models.secr;

namespace DataContext
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {
        }


        #region --------------Dictionary -----------------


        #region product
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
        public DbSet<product_property_type> fdc_products_properties_types { get; set; }

        /// <summary>
        /// Справочник типов продукции
        /// </summary>
        public DbSet<product_type> fdc_products_types { get; set; }

        #region rate
        /// <summary>
        /// Справочник оценки продукции (рйтинг)
        /// </summary>
        public DbSet<product_rate> fdc_products_rates { get; set; }

        /// <summary>
        /// Справочник отзывов и предложений по продуктам
        /// </summary>
        public DbSet<product_response> fdc_products_responses { get; set; }

        /// <summary>
        /// Справочник отзывов и предложений по продуктам
        /// </summary>
        public DbSet<product_question> fdc_products_questions { get; set; }
        #endregion

        #endregion

        #region contractors

        /// <summary>
        /// Справочник контрагентов (поставщиков)
        /// </summary>
        public DbSet<contractor> fdc_contractors { get; set; }

        #region rate
        /// <summary>
        /// Справочник оценки контрагентов (рйтинг)
        /// </summary>
        public DbSet<contractor_rate> fdc_contractors_rates { get; set; }

        /// <summary>
        /// Справочник отзывов и предложений по контрагенту
        /// </summary>
        public DbSet<contractor_response> fdc_contractors_responses { get; set; }

        #endregion

        #endregion

        #region users

        /// <summary>
        /// Справочник зарегистрированных пользователей
        /// </summary>
        public DbSet<user> fdc_users { get; set; }


        /// <summary>
        /// Справочник роли
        /// </summary>
        public DbSet<role> fdc_roles { get; set; }

        #endregion

        /// <summary>
        /// Общероссийский классификатор единиц измерения
        /// </summary>
        public DbSet<okei> fdc_okei { get; set; }

        /// <summary>
        /// Общее хранилище изображений для всех справочников
        /// </summary>
        public DbSet<image> fdc_images { get; set; }

        /// <summary>
        /// Разделы справочника материалов и оборудования
        /// </summary>
        public DbSet<product_directory> fdc_product_directories { get; set; }
        
        
        
        #endregion
    }
}
