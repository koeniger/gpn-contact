//#define Role
//#define Contractor
//#define User
//#define OKEI
//#define ProductDirectory
//#define ProductType
//#define ProductProperty
//#define Product
//#define ProductValue
//#define ProductQuestion
//#define ProductRate
//#define ProductResponse
#define ContractorRate
#define ContractorResponse


using DataBaseUpdater.DataContext;
using Models.gpn;
using Models.secr;
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

        /// <summary>
        /// Тестовое наполнение таблиц
        /// </summary>
        /// <param name="context"></param>
        public void TestFillingOfTables(UpdateContext context)
        {
            FillRole(context);
        }

        void FillRole(UpdateContext context)
        {
            #region Role
#if Role
            try
            {
                var roles = new List<role>()
                {
                    new role(){ role_id = 0, role_name = "admin", description = "DB administrator"},
                    new role(){ role_id = 0, role_name = "user", description = "DB user"},
                    new role(){ role_id = 0, role_name = "contractor", description = "contractor"}
                };

                context.fdc_roles.AddRange(roles);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region Contractor
#if Contractor
            try
            {
                var contractors = new List<contractor>()
                {
                    new contractor()
                    { 
                        contractor_id = 0,
                        contractor_name = "АВТОХИМ-РОСТ",
                        description = "ООО ПК \"АВТОХИМ-РОСТ\" ",
                        contact_info = "Новосибирск"
                    },
                    new contractor()
                    {
                        contractor_id = 0,
                        contractor_name = "Астерос",
                        description = "Астерос, Группа Компаний",
                        contact_info = "Москва"
                    },
                    new contractor()
                    {
                        contractor_id = 0,
                        contractor_name = "СмартВес",
                        description = "СмартВес, ООО, научно-производственная фирма",
                        contact_info = "Санкт-Петербург"
                    },
                };

                context.fdc_contractors.AddRange(contractors);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region User
#if User
            try
            {
                var users = new List<user>()
                {
                    new user()
                    {
                        user_id = 0, 
                        email = "avtohimrost@mail.ru",
                        user_name = "Олег Иванов",
                        position = "Менеджер по продажам",
                        contact_info = "ул. Дунаевского,25, офис 2 (1-й этаж), Новосибирск, Новосибирская область, 630027, Россия, +7 (913) 727-90-71",
                        role_id = 3,
                        contractor_id = 1,
                        Password=""
                    },
                    new user()
                    {
                        user_id = 0,
                        email = "info@asteros.ru",
                        user_name = "Илья Петров",
                        position = "Директор",
                        contact_info = "Москва, Новохохловская, 23 - 5 этаж, БЦ Ring Park, +7 (495) 787-24-50",
                        role_id = 3,
                        contractor_id = 2,
                        Password=""
                    },
                    new user()
                    {
                        user_id = 0,
                        email = "smartweight@mail.ru",
                        user_name = "Екатерина Преживальская",
                        position = "Менеджер по продажам",
                        contact_info = "Санкт-Петербург, Маршала Говорова, 37",
                        role_id = 3,
                        contractor_id = 3,
                        Password=""
                    }
                };

                context.fdc_users.AddRange(users);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region OKEI
#if OKEI
            try
            {
                var okeis = new List<okei>()
                {
                    new okei()
                    {
                        okei_id = 0,
                        name_short = "°",
                        name_full = "Температура, градус Цельсия"
                    },
                    new okei()
                    {
                        okei_id = 0,
                        name_short = "т",
                        name_full = "Вес, тонна"
                    },
                    new okei()
                    {
                        okei_id = 0,
                        name_short = "м³",
                        name_full = "Объем, метры кубические"
                    },
                    new okei()
                    {
                        okei_id = 0,
                        name_short = "шт.",
                        name_full = "Количество, штук"
                    }
                };

                context.fdc_okei.AddRange(okeis);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ProductDirectory

#if ProductDirectory
            try
            {
                var mainDirectories = new List<product_directory>()
                {
                    new product_directory()
                    {
                        product_directory_id = 0,
                        product_directory_name = "Товары общего применения",
                        description = "Прочие товары",

                        parent_id = null,
                        parent = null
                    },
                    new product_directory()
                    {
                        product_directory_id = 0,
                        product_directory_name = "Программное обеспечение",
                        description = "Программное обеспечение для ПК",

                        parent_id = null,
                        parent = null
                    }
                };

                context.fdc_product_directories.AddRange(mainDirectories);

                context.SaveChanges();

                var softs = context.fdc_product_directories.FirstOrDefault(i => i.product_directory_name == "Товары общего применения");
                var products = context.fdc_product_directories.FirstOrDefault(i => i.product_directory_name == "Программное обеспечение");

                var productsDirectories = new List<product_directory>()
                {
                    new product_directory()
                    {
                        product_directory_id = 0,
                        product_directory_name = "Весы",
                        description = "Весы",

                        parent_id = products.product_directory_id,
                        parent = products
                    },
                    new product_directory()
                    {
                        product_directory_id = 0,
                        product_directory_name = "Антисепик",
                        description = "Антисептик универсальный",

                        parent_id = products.product_directory_id,
                        parent = products
                    },
                    new product_directory()
                    {
                        product_directory_id = 0,
                        product_directory_name = "Asteros.EDU",
                        description = "Asteros для ВУЗов",

                        parent_id = softs.product_directory_id,
                        parent = softs
                    }
                };


                context.fdc_product_directories.AddRange(productsDirectories);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ProductType
#if ProductType
            try
            {
                var productType = new List<product_type>()
                {
                    new product_type()
                    {
                        product_type_id = 0,
                        product_type_name = "Химия",
                        is_archive = false,
                        date_change = DateTime.Now,
                        date_archive = DateTime.Now
                    },
                    new product_type()
                    {
                        product_type_id = 0,
                        product_type_name = "Программное обеспечение",
                        is_archive = false,
                        date_change = DateTime.Now,
                        date_archive = DateTime.Now
                    },
                    new product_type()
                    {
                        product_type_id = 0,
                        product_type_name = "Оборудование",
                        is_archive = false,
                        date_change = DateTime.Now,
                        date_archive = DateTime.Now
                    },
                };

                context.fdc_products_types.AddRange(productType);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ProductProperty
#if ProductProperty
            try
            {
                var weight = context.fdc_okei.FirstOrDefault(i => i.name_full == "Вес, тонна");
                var tank = context.fdc_okei.FirstOrDefault(i => i.name_full == "Объем, метры кубические");
                var amount = context.fdc_okei.FirstOrDefault(i => i.name_full == "Количество, штук");

                var chem = context.fdc_products_types.FirstOrDefault(i => i.product_type_name == "Химия");
                var soft = context.fdc_products_types.FirstOrDefault(i => i.product_type_name == "Программное обеспечение");
                var equip = context.fdc_products_types.FirstOrDefault(i => i.product_type_name == "Оборудование");

                var productProperty = new List<product_property>()
                {
                    new product_property()
                    {
                        product_property_id = 0,
                        property_name = "Антисептики",
                        value_type = "Объем",
                        okei_id = tank.okei_id,
                        okei = tank,
                        product_type_id = chem.product_type_id,
                        product_type = chem
                    },
                    new product_property()
                    {
                        product_property_id = 0,
                        property_name = "для Windows",
                        value_type = "шт.",
                        okei_id = amount.okei_id,
                        okei = amount,
                        product_type_id = soft.product_type_id,
                        product_type = soft
                    },
                    new product_property()
                    {
                        product_property_id = 0,
                        property_name = "Весовое оборудование",
                        value_type = "шт.",
                        okei_id = weight.okei_id,
                        okei = weight,
                        product_type_id = equip.product_type_id,
                        product_type = equip
                    },
                };

                context.fdc_products_properties.AddRange(productProperty);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region Product
#if Product
            try
            {
                var chem = context.fdc_products_types.FirstOrDefault(i => i.product_type_name == "Химия");
                var soft = context.fdc_products_types.FirstOrDefault(i => i.product_type_name == "Программное обеспечение");
                var equip = context.fdc_products_types.FirstOrDefault(i => i.product_type_name == "Оборудование");

                var chemDir = context.fdc_product_directories.FirstOrDefault(i => i.product_directory_name == "Антисепик");
                var softDir = context.fdc_product_directories.FirstOrDefault(i => i.product_directory_name == "Asteros.EDU");
                var equipDir = context.fdc_product_directories.FirstOrDefault(i => i.product_directory_name == "Весы");

                var chemContractor = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "АВТОХИМ-РОСТ");
                var softContractor = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "Астерос");
                var equipContractor = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "СмартВес");

                var product = new List<product>()
                {
                    new product()
                    {
                        product_id = 0,
                        product_name = "Чистота",
                        description_short = "Антисептик",
                        description_full = "Антисептик \"Чистота\"",
                        price = Price.GetRandomPrice(),
                        is_archive = false,
                        date_change = DateTime.Now,
                        date_archive = DateTime.Now,
                        product_directory_id = chemDir.product_directory_id,
                        product_directory = chemDir,
                        contractor_id = chemContractor.contractor_id,
                        contractor = chemContractor,
                        product_type_id = chem.product_type_id,
                        product_type = chem
                    },
                    new product()
                    {
                        product_id = 0,
                        product_name = "Asteros.EDU",
                        description_short = "Asteros.EDU",
                        description_full = "Программное обеспечение для Windows \"Asteros.EDU\"",
                        price = Price.GetRandomPrice(),
                        is_archive = false,
                        date_change = DateTime.Now,
                        date_archive = DateTime.Now,
                        product_directory_id = softDir.product_directory_id,
                        product_directory = softDir,
                        contractor_id = softContractor.contractor_id,
                        contractor = softContractor,
                        product_type_id = soft.product_type_id,
                        product_type = soft
                    },
                    new product()
                    {
                        product_id = 0,
                        product_name = "Умные весы",
                        description_short = "Весы",
                        description_full = "Умные весы",
                        price = Price.GetRandomPrice(),
                        is_archive = false,
                        date_change = DateTime.Now,
                        date_archive = DateTime.Now,
                        product_directory_id = equipDir.product_directory_id,
                        product_directory = equipDir,
                        contractor_id = equipContractor.contractor_id,
                        contractor = equipContractor,
                        product_type_id = equip.product_type_id,
                        product_type = equip
                    },
                };

                context.fdc_products.AddRange(product);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ProductValue
#if ProductValue
            try
            {
                var weight = context.fdc_products.FirstOrDefault(i => i.product_name == "Умные весы");
                var chem = context.fdc_products.FirstOrDefault(i => i.product_name == "Чистота");
                var soft = context.fdc_products.FirstOrDefault(i => i.product_name == "Asteros.EDU");

                var chemProperty = context.fdc_products_properties.FirstOrDefault(i => i.property_name == "Антисептики");
                var softProperty = context.fdc_products_properties.FirstOrDefault(i => i.property_name == "для Windows");
                var equipProperty = context.fdc_products_properties.FirstOrDefault(i => i.property_name == "Весовое оборудование");

                var productValue = new List<product_value>()
                {
                    new product_value()
                    {
                        product_value_id = 0,
                        value = "Бочка",
                        product_id = chem.product_id,
                        product = chem,
                        product_property_id = chemProperty.product_property_id,
                        product_property = chemProperty
                    },
                    new product_value()
                    {
                        product_value_id = 0,
                        value = "Диск",
                        product_id = soft.product_id,
                        product = soft,
                        product_property_id = softProperty.product_property_id,
                        product_property = softProperty
                    },
                    new product_value()
                    {
                        product_value_id = 0,
                        value = "Весы",
                        product_id = weight.product_id,
                        product = weight,
                        product_property_id = equipProperty.product_property_id,
                        product_property = equipProperty
                    },
                };

                context.fdc_products_values.AddRange(productValue);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ProductQuestion
#if ProductQuestion
            try
            {
                var weight = context.fdc_products.FirstOrDefault(i => i.product_name == "Умные весы");
                var chem = context.fdc_products.FirstOrDefault(i => i.product_name == "Чистота");
                var soft = context.fdc_products.FirstOrDefault(i => i.product_name == "Asteros.EDU");

                var oleg = context.fdc_users.FirstOrDefault(i => i.user_name == "Олег Иванов");
                var ilya = context.fdc_users.FirstOrDefault(i => i.user_name == "Илья Петров");
                var cat = context.fdc_users.FirstOrDefault(i => i.user_name == "Екатерина Преживальская");


                var mainQuestions = new List<product_question>()
                {
                    new product_question()
                    {
                        product_question_id = 0,
                        question = "Эти весы предназначены для работы в минус 40?",
                        questions_date = DateTime.Now,
                        product_id = weight.product_id,
                        product = weight,
                        user_id = oleg.user_id,
                        user = oleg,
                        parent_id = null,
                        parent = null
                    },
                    new product_question()
                    {
                        product_question_id = 0,
                        question = "Этот анисептик помогает от Коронавируса?",
                        questions_date = DateTime.Now,
                        product_id = chem.product_id,
                        product = chem,
                        user_id = ilya.user_id,
                        user = ilya,
                        parent_id = null,
                        parent = null
                    }
                };

                context.fdc_products_questions.AddRange(mainQuestions);

                context.SaveChanges();

                var question1 = context.fdc_products_questions.FirstOrDefault(i => i.question == "Эти весы предназначены для работы в минус 40?");
                var question2 = context.fdc_products_questions.FirstOrDefault(i => i.question == "Этот анисептик помогает от Коронавируса?");

                var Questions = new List<product_question>()
                {
                    new product_question()
                    {
                        product_question_id = 0,
                        question = "Да.",
                        questions_date = DateTime.Now,
                        product_id = question1.product_id,
                        product = question1.product,
                        user_id = cat.user_id,
                        user = cat,
                        parent_id = question1.product_question_id,
                        parent = question1
                    },
                    new product_question()
                    {
                        product_question_id = 0,
                        question = "Нет, но есть другой, утвержденный РосПотребНадзором",
                        questions_date = DateTime.Now,
                        product_id = question2.product_id,
                        product = question2.product,
                        user_id = cat.user_id,
                        user = cat,
                        parent_id = question2.product_question_id,
                        parent = question2
                    }
                };

                context.fdc_products_questions.AddRange(Questions);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ProductRate
#if ProductRate
            try
            {
                var weight = context.fdc_products.FirstOrDefault(i => i.product_name == "Умные весы");
                var chem = context.fdc_products.FirstOrDefault(i => i.product_name == "Чистота");
                var soft = context.fdc_products.FirstOrDefault(i => i.product_name == "Asteros.EDU");

                var oleg = context.fdc_users.FirstOrDefault(i => i.user_name == "Олег Иванов");
                var ilya = context.fdc_users.FirstOrDefault(i => i.user_name == "Илья Петров");
                var cat = context.fdc_users.FirstOrDefault(i => i.user_name == "Екатерина Преживальская");


                var Rates = new List<product_rate>()
                {
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 5,
                        product_id = weight.product_id,
                        product = weight,
                        user_id = oleg.user_id,
                        user = oleg,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 2,
                        product_id = weight.product_id,
                        product = weight,
                        user_id = ilya.user_id,
                        user = ilya,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 3,
                        product_id = weight.product_id,
                        product = weight,
                        user_id = cat.user_id,
                        user = cat,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 5,
                        product_id = chem.product_id,
                        product = chem,
                        user_id = oleg.user_id,
                        user = oleg,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 2,
                        product_id = chem.product_id,
                        product = chem,
                        user_id = ilya.user_id,
                        user = ilya,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 4,
                        product_id = chem.product_id,
                        product = chem,
                        user_id = cat.user_id,
                        user = cat,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 3,
                        product_id = soft.product_id,
                        product = soft,
                        user_id = oleg.user_id,
                        user = oleg,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 2,
                        product_id = soft.product_id,
                        product = soft,
                        user_id = ilya.user_id,
                        user = ilya,
                        rate_date = DateTime.Now
                    },
                    new product_rate()
                    {
                        product_rate_id = 0,
                        rate = 3,
                        product_id = soft.product_id,
                        product = soft,
                        user_id = cat.user_id,
                        user = cat,
                        rate_date = DateTime.Now
                    }
                };

                context.fdc_products_rates.AddRange(Rates);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ProductResponse
#if ProductResponse
            try
            {
                var weight = context.fdc_products.FirstOrDefault(i => i.product_name == "Умные весы");
                var chem = context.fdc_products.FirstOrDefault(i => i.product_name == "Чистота");
                var soft = context.fdc_products.FirstOrDefault(i => i.product_name == "Asteros.EDU");

                var oleg = context.fdc_users.FirstOrDefault(i => i.user_name == "Олег Иванов");
                var ilya = context.fdc_users.FirstOrDefault(i => i.user_name == "Илья Петров");
                var cat = context.fdc_users.FirstOrDefault(i => i.user_name == "Екатерина Преживальская");


                var mainProductResponse = new List<product_response>()
                {
                    new product_response()
                    {
                        product_response_id = 0,
                        response = "У этих весов большая погрешность",
                        response_date = DateTime.Now,
                        product_id = weight.product_id,
                        product = weight,
                        user_id = oleg.user_id,
                        user = oleg,
                        parent_id = null,
                        parent = null
                    },
                    new product_response()
                    {
                        product_response_id = 0,
                        response = "Антисептик пахнет спиртом",
                        response_date = DateTime.Now,
                        product_id = chem.product_id,
                        product = chem,
                        user_id = ilya.user_id,
                        user = ilya,
                        parent_id = null,
                        parent = null
                    }
                };

                context.fdc_products_responses.AddRange(mainProductResponse);

                context.SaveChanges();

                var response1 = context.fdc_products_responses.FirstOrDefault(i => i.product_id == weight.product_id);
                var response2 = context.fdc_products_responses.FirstOrDefault(i => i.product_id == chem.product_id);

                var ProductResponse = new List<product_response>()
                {
                    new product_response()
                    {
                        product_response_id = 0,
                        response = "Соответсвует ГОСТу",
                        response_date = DateTime.Now,
                        product_id = response1.product_id,
                        product = response1.product,
                        user_id = cat.user_id,
                        user = cat,
                        parent_id = response1.product_response_id,
                        parent = response1
                    },
                    new product_response()
                    {
                        product_response_id = 0,
                        response = "Да, это ведь антисептик.",
                        response_date = DateTime.Now,
                        product_id = response2.product_id,
                        product = response2.product,
                        user_id = cat.user_id,
                        user = cat,
                        parent_id = response2.product_response_id,
                        parent = response2
                    }
                };

                context.fdc_products_responses.AddRange(ProductResponse);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

            #region ContractorRate
#if ContractorRate
            try
            {
                var weight = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "СмартВес");
                var chem = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "АВТОХИМ-РОСТ");
                var soft = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "Астерос");

                var oleg = context.fdc_users.FirstOrDefault(i => i.user_name == "Олег Иванов");
                var ilya = context.fdc_users.FirstOrDefault(i => i.user_name == "Илья Петров");
                var cat = context.fdc_users.FirstOrDefault(i => i.user_name == "Екатерина Преживальская");


                var Rates = new List<contractor_rate>()
                {
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 5,
                        contractor_id = weight.contractor_id,
                        contractor = weight,
                        user_id = oleg.user_id,
                        user = oleg,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 2,
                        contractor_id = weight.contractor_id,
                        contractor = weight,
                        user_id = ilya.user_id,
                        user = ilya,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 3,
                        contractor_id = weight.contractor_id,
                        contractor = weight,
                        user_id = cat.user_id,
                        user = cat,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 5,
                        contractor_id = chem.contractor_id,
                        contractor = chem,
                        user_id = oleg.user_id,
                        user = oleg,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 2,
                        contractor_id = chem.contractor_id,
                        contractor = chem,
                        user_id = ilya.user_id,
                        user = ilya,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 4,
                        contractor_id = chem.contractor_id,
                        contractor = chem,
                        user_id = cat.user_id,
                        user = cat,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 3,
                        contractor_id = soft.contractor_id,
                        contractor = soft,
                        user_id = oleg.user_id,
                        user = oleg,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 2,
                        contractor_id = soft.contractor_id,
                        contractor = soft,
                        user_id = ilya.user_id,
                        user = ilya,
                        rate_date = DateTime.Now
                    },
                    new contractor_rate()
                    {
                        contractor_rate_id = 0,
                        rate = 3,
                        contractor_id = soft.contractor_id,
                        contractor = soft,
                        user_id = cat.user_id,
                        user = cat,
                        rate_date = DateTime.Now
                    }
                };

                context.fdc_contractors_rates.AddRange(Rates);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion
           
            #region ContractorResponse
#if ContractorResponse
            try
            {
                var weight = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "СмартВес");
                var chem = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "АВТОХИМ-РОСТ");
                var soft = context.fdc_contractors.FirstOrDefault(i => i.contractor_name == "Астерос");

                var oleg = context.fdc_users.FirstOrDefault(i => i.user_name == "Олег Иванов");
                var ilya = context.fdc_users.FirstOrDefault(i => i.user_name == "Илья Петров");
                var cat = context.fdc_users.FirstOrDefault(i => i.user_name == "Екатерина Преживальская");


                var mainProductResponse = new List<contractor_response>()
                {
                    new contractor_response()
                    {
                        contractor_response_id = 0,
                        response = "Хороший поставщик",
                        response_date = DateTime.Now,
                        contractor_id = weight.contractor_id,
                        contractor = weight,
                        user_id = oleg.user_id,
                        user = oleg,
                        parent_id = null,
                        parent = null
                    },
                    new contractor_response()
                    {
                        contractor_response_id = 0,
                        response = "Упаковка не надежная",
                        response_date = DateTime.Now,
                        contractor_id = chem.contractor_id,
                        contractor = chem,
                        user_id = ilya.user_id,
                        user = ilya,
                        parent_id = null,
                        parent = null
                    }
                };

                context.fdc_contractors_responses.AddRange(mainProductResponse);

                context.SaveChanges();

                var response1 = context.fdc_contractors_responses.FirstOrDefault(i => i.contractor_id == weight.contractor_id);
                var response2 = context.fdc_contractors_responses.FirstOrDefault(i => i.contractor_id == chem.contractor_id);

                var ContractorResponse = new List<contractor_response>()
                {
                    new contractor_response()
                    {
                        contractor_response_id = 0,
                        response = "Соответсвует ГОСТу",
                        response_date = DateTime.Now,
                        contractor_id = response1.contractor_id,
                        contractor = response1.contractor,
                        user_id = cat.user_id,
                        user = cat,
                        parent_id = response1.contractor_response_id,
                        parent = response1
                    },
                    new contractor_response()
                    {
                        contractor_response_id = 0,
                        response = "Да, это ведь антисептик.",
                        response_date = DateTime.Now,
                        contractor_id = response2.contractor_id,
                        contractor = response2.contractor,
                        user_id = cat.user_id,
                        user = cat,
                        parent_id = response2.contractor_response_id,
                        parent = response2
                    }
                };

                context.fdc_contractors_responses.AddRange(ContractorResponse);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            #endregion

        }
    }
}
