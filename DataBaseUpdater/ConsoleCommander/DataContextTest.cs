using DataBaseUpdater.DataContext;
using Models.gpn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseUpdater.ConsoleCommander
{
    public class DataContextTest
    {
        /// <summary>
        /// Добавляет продукт в справочник Products
        /// </summary>
        /// <param name="context"></param>
        public void Add(UpdateContext context)
        {
            try
            {
                bool tryParse = false;
                int id = 0;

                while (!tryParse)
                {
                    Console.WriteLine("Get Product Type Id");
                    Console.WriteLine("Enter choice (\"exit\" - for exit): ");

                    string produtTypeId = Console.ReadLine();

                    //Если пользователь ввел exit - то выход
                    if (produtTypeId == "exit") return;

                    tryParse = Int32.TryParse(produtTypeId, out id);
                }

                var produtType = context.fdc_products_types.FirstOrDefault(p => p.product_type_id == id);

                if (produtType == null)
                {
                    Console.WriteLine("Null Product Type Id");
                    return;
                }

                var count = context.fdc_products.Count();

                var products = new List<product>() {
                new product(){
                    product_name = $"Продукт {count}",
                    description_short = $"{count}",
                    description_full = $"Проект Контакт, тестовый продукт {count}",
                    date_change = DateTime.Now,
                    is_archive = true,
                    price = Price.GetRandomPrice(),
                    product_type =  produtType,
                    product_type_id = produtType.product_type_id},
                };

                produtType.Products = products;

                context.fdc_products_types.Update(produtType);

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                ConsoleWriter.ErrorLine(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет продукт в справочник Products
        /// </summary>
        /// <param name="context"></param>
        public void Remove(UpdateContext context)
        {
            try
            {
                bool tryParse = false;
                int id = 0;

                //получение id продукта
                while (!tryParse)
                {
                    Console.WriteLine("Get Product Id for Removing ");
                    Console.WriteLine("Enter choice (\"exit\" - for exit): ");

                    string produtTypeId = Console.ReadLine();

                    //Если пользователь ввел exit - то выход
                    if (produtTypeId == "exit") return;

                    tryParse = Int32.TryParse(produtTypeId, out id);
                }

                //получение обхекта по id
                var item = context.fdc_products.Where(p => p.product_id == id).FirstOrDefault();

                if (item != null)
                {
                    //удаление продукта из бд
                    context.fdc_products.Remove(item);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ConsoleWriter.ErrorLine(ex.Message);
            }
        }
    }
}
