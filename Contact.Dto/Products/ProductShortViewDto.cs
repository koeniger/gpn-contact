using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Dto.Products
{
    /// <summary>
    /// Краткое представление продукта
    /// </summary>
    public class ProductShortViewDto
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Наименование директории в которой расположен продукт
        /// </summary>
        public string DirectoryName { get; set; }
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string DescriptionShort { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewCount { get; set; }
        /// <summary>
        /// Рейтинг
        /// </summary>
        public float Rating{ get; set; }
        /// <summary>
        /// Количество поставок
        /// </summary>
        public int SupplyCount { get; set; }
    }
}
